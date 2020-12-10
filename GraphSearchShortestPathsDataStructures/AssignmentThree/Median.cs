using Utility.DataStructures;

namespace GraphSearchShortestPathsDataStructures.AssignmentThree
{
    public class Median
    {
        private readonly MinHeap<int> _largerHalf = new();
        private readonly MaxHeap<int> _smallHalf = new();

        public int GetMedian(int i)
        {
            var topOfSmallHalf = _smallHalf.Peek();
            if (i <= topOfSmallHalf)
                _smallHalf.Insert(i);
            else
                _largerHalf.Insert(i);
            EnsureHeapBalances();
            var lengthOfSmallHalf = _smallHalf.Count;
            var lengthOfLargerHalf = _largerHalf.Count;
            return lengthOfLargerHalf > lengthOfSmallHalf ? _largerHalf.Peek() : _smallHalf.Peek();
        }

        private void EnsureHeapBalances()
        {
            var lengthOfSmallHalf = _smallHalf.Count;
            var lengthOfLargerHalf = _largerHalf.Count;
            if (lengthOfLargerHalf - lengthOfSmallHalf > 1)
                _smallHalf.Insert(_largerHalf.Pull());
            if (lengthOfSmallHalf - lengthOfLargerHalf > 1)
                _largerHalf.Insert(_smallHalf.Pull());
        }
    }
}