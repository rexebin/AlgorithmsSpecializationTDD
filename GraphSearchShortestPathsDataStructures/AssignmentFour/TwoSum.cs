using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphSearchShortestPathsDataStructures.AssignmentFour
{
    public class TwoSum
    {
        public readonly Dictionary<long, long[]> Input;
        public readonly HashSet<long> HasTwoSum = new();

        public TwoSum(long[] input)
        {
            Input = input.GroupBy(x => Math.Abs(x) / 10000)
                .Where(x => x.Count() >= 2)
                .ToDictionary(x => x.Key,
                    e => e.ToArray());
        }

        public void GetCount(long[] input)
        {
            for (var index = 0; index < input.Length; index++)
            {
                var v = input[index];
                for (var i = 0; i < input.Length; i++)
                {
                    if (index == i) continue;
                    var v1 = input[i];
                    var sum = v + v1;
                    if (sum >= -10000 && sum <= 10000)
                    {
                        HasTwoSum.Add(sum);
                    }
                }
            }
        }

        public int GetAllCount()
        {
            foreach (var i in Input)
            {
                GetCount(i.Value);
            }

            return HasTwoSum.Count;
        }
    }
}