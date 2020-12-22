using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class Mwis
    {
        public List<int> Weights { get; }
        private List<int> MaxWeights { get; set; } = new();

        public Mwis(List<int> weights)
        {
            Weights = weights;
        }

        public string GetAnswer()
        {
            var maxIndependentWeight = GetMaxIndependentWeight();
            var requestedIndexes = new[] {1, 2, 3, 4, 17, 117, 517, 997};
            return string.Join("",
                requestedIndexes.Select(x =>
                    maxIndependentWeight.TryGetValue(x, out _) ? "1" : "0"));
        }

        public Dictionary<int, int> GetMaxIndependentWeight()
        {
            MaxWeights = GetMaxWeights();
            var maxWeightIndependentSet = new Dictionary<int, int>();
            var i = MaxWeights.Count - 1;
            while (i >= 1)
            {
                if (MaxWeights[i] > MaxWeights[i - 1])
                {
                    maxWeightIndependentSet.Add(i, Weights[i - 1]);
                    i -= 2;
                    continue;
                }

                i -= 1;
            }

            return maxWeightIndependentSet;
        }

        public List<int> GetMaxWeights()
        {
            var result = new List<int> {0, Weights[0]};
            for (var i = 1; i < Weights.Count; i++)
                result.Add(Math.Max(result[i], result[i - 1] + Weights[i]));
            return result;
        }

        public static List<int> ReadFile(string filename)
        {
            return new FileReader().ReadFile(filename).Skip(1).Select(int.Parse).ToList();
        }
    }
}