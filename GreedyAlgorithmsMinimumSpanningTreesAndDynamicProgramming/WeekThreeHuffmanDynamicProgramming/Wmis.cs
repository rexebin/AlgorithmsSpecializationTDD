using System;
using System.Collections.Generic;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class Wmis
    {
        public int[] GetMaxWeights(int[] weights)
        {
            var result = new List<int> {0, weights[0]};

            for (var i = 1; i < weights.Length; i++)
                result.Add(Math.Max(result[i], result[i - 1] + weights[i]));
            return result.ToArray();
        }

        public Dictionary<int, int> GetMaxIndependentWeight(int[] maxWeights, int[] weights)
        {
            var maxWeightIndependentSet = new Dictionary<int, int>();

            for (int i = maxWeights.Length - 1; i >= 1;)
                if (maxWeights[i] > maxWeights[i - 1])
                {
                    Console.WriteLine(weights[i - 1]);
                    maxWeightIndependentSet.Add(i, weights[i - 1]);
                    i -= 2;
                }
                else i -= 1;

            return maxWeightIndependentSet;
        }
    }
}