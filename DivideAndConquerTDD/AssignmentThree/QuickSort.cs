using System;
using System.IO;
using System.Linq;

namespace DivideAndConquerTDD.AssignmentThree
{
    public enum PivotMode
    {
        FirstElement,
        LastElement,
        Median,
        Random
    }

    public class QuickSort
    {
        private long _count;

        public int[] Sort(int[] input, int left, int right, PivotMode mode = PivotMode.FirstElement)
        {
            _count += right - left - 1;
            input = SwapPivotToFirst(input, left, right, mode);
            var splitIndex = PartitionAndReturnSplitIndex(input, left, right);
            if (splitIndex - 1 > left)
                Sort(input, left, splitIndex - 1, mode);

            if (splitIndex < right - 1)
                Sort(input, splitIndex, right, mode);

            return input;
        }

        private int PartitionAndReturnSplitIndex(int[] input, int left, int right)
        {
            var pivot = input[left];
            var splitIndex = left + 1;
            for (int index = left + 1; index < right; index++)
            {
                if (input[index] > pivot)
                    continue;
                if (index != splitIndex)
                {
                    var oldI = input[splitIndex];
                    input[splitIndex] = input[index];
                    input[index] = oldI;
                }

                splitIndex++;
            }

            input[left] = input[splitIndex - 1];
            input[splitIndex - 1] = pivot;
            return splitIndex;
        }

        private int[] SwapPivotToFirst(int[] input, int left, int right, PivotMode mode)
        {
            return mode switch
            {
                PivotMode.LastElement => SwapLastAndFirstElement(input, left, right),
                PivotMode.Median => SwapMedianWithFirstElement(input, left, right),
                PivotMode.Random => SwapRandomElementWithFirstElement(input, left, right),
                _ => input
            };
        }

        public long Sort(PivotMode mode = PivotMode.FirstElement)
        {
            var input = File.ReadAllLines(GetPath())
                .Select(int.Parse).ToArray();
            var sorted = Sort(input, 0, input.Length, mode);
            return _count;
        }

        private string GetPath()
        {
            return Path.GetFullPath(@"..\..\..\AssignmentThree\QuickSort.txt");
        }

        public long GetCount()
        {
            return _count;
        }

        public int[] SwapLastAndFirstElement(int[] input, int left, int right)
        {
            var lastElement = input[right - 1];
            input[right - 1] = input[left];
            input[left] = lastElement;
            return input;
        }

        public int[] SwapMedianWithFirstElement(int[] input, int left, int right)
        {
            var medianIndex = GetMedianIndex(input, left, right);
            var firstElement = input[left];
            input[left] = input[medianIndex];
            input[medianIndex] = firstElement;
            return input;
        }

        public int[] SwapRandomElementWithFirstElement(int[] input, int left, int right)
        {
            var randomIndex = new Random().Next(left, right);
            var firstElement = input[left];
            input[left] = input[randomIndex];
            input[randomIndex] = firstElement;
            return input;
        }

        public int GetMiddleIndex(int[] input)
        {
            return input.Length / 2 - (1 - input.Length % 2);
        }

        public int GetMedianIndex(int[] input, int left, int right)
        {
            var medianIndex = GetMiddleIndex(input);
            if (input[left] > input[medianIndex] && input[left] < input[right - 1] ||
                input[left] > input[right - 1] && input[left] < input[medianIndex])
                return left;

            if (input[medianIndex] > input[left] && input[medianIndex] < input[right - 1] ||
                input[medianIndex] > input[right - 1] && input[medianIndex] < input[left]
            )
                return medianIndex;

            return right - 1;
        }
    }
}