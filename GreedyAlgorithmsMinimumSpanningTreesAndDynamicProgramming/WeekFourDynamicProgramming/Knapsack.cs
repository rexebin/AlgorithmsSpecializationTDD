using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekFourDynamicProgramming
{
    public class Knapsack
    {
        public List<List<int>> ProcessItems(List<Item> items, int maxWeight)
        {
            var result = new List<List<int>> {Initialize(maxWeight)};
            for (var i = 1; i <= items.Count; i++)
            {
                result.Add(Initialize(maxWeight));
                for (var j = 0; j <= maxWeight; j++)
                {
                    var remainingWeight = j - items[i - 1].Weight;
                    if (remainingWeight < 0)
                    {
                        result[i][j] = result[i - 1][j];
                        continue;
                    }

                    result[i][j] = Math.Max(result[i - 1][j],
                        result[i - 1][remainingWeight] + items[i - 1].Value);
                }
            }

            return result;
        }

        private static List<int> Initialize(int maxWeight)
        {
            return Enumerable.Range(0, maxWeight+1).Select(x => 0).ToList();
        }

        public List<Item> ReadFile(string filename)
        {
            return new FileReader().ReadFile(filename)
                .Skip(1)
                .Select(x => x.Split(" ").Select(int.Parse).ToList())
                .Select(x => new Item(x[0], x[1])).ToList();
        }

        public int GetOptimalResult(string filename, int maxWeight)
        {
            var items = ReadFile(filename);
            var result = ProcessItems(items, maxWeight);
            return result.Last().Last();
        }
    }
}