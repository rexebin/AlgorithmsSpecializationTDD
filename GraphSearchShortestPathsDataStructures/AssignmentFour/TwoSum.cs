using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace GraphSearchShortestPathsDataStructures.AssignmentFour
{
    public class TwoSum
    {
        public ImmutableSortedSet<long> UniqueNumbers { get; }
        public HashSet<long> Duplications { get; }

        public TwoSum(long[] input)
        {
            UniqueNumbers = input.ToImmutableSortedSet();
            Duplications = input.GroupBy(e => e)
                .Where(e => e.Count() >= 2)
                .SelectMany(e => e)
                .ToHashSet();
        }

        public int GetCount()
        {
            var numbersWithTwoSums = new HashSet<long>();
            foreach (var n in UniqueNumbers)
            {
                var lowerBoundIndex = GetClosestIndex(-10000 - n);
                var highBoundIndex = GetClosestIndex(10000 - n);
                for (var i = lowerBoundIndex; i < highBoundIndex; i++)
                {
                    if (UniqueNumbers[i] != n) numbersWithTwoSums.Add(UniqueNumbers[i] + n);
                }
            }

            foreach (var n in Duplications.Where(n => n * 2 >= -10000 && n * 2 <= 10000))
            {
                numbersWithTwoSums.Add(n * 2);
            }

            return numbersWithTwoSums.Count;
        }

        private int GetClosestIndex(long value)
        {
            var index = UniqueNumbers.IndexOf(value);
            return index > 0 ? index : UniqueNumbers.Add(value).IndexOf(value);
        }
    }
}