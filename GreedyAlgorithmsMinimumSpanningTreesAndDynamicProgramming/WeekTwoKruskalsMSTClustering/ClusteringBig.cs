using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;
using Utility.UnionFind;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class ClusteringBig
    {
        public int GetClusterQuantity(string filename, int distance)
        {
            var graph = ReadFile(filename);
            var clusters = GetInitialClusters(graph);
            var clustersCount = clusters.Count;
            foreach (var vertex1 in clusters)
            {
                var nodes = GetNodesWithDistanceLessThanThree(vertex1.Key.ToCharArray(), graph);
                foreach (var node in nodes)
                {
                    var vertex = clusters[node];
                    if (vertex1.Value.IsSameUnion(vertex))
                        continue;

                    vertex1.Value.Union(vertex);
                    clustersCount--;
                }
            }

            return clustersCount;
        }

        private List<string> GetNodesWithDistanceLessThanThree(char[] input, HashSet<string> graph)
        {
            var combinations = new List<string>();
            for (int i = 1; i < 3; i++) combinations.AddRange(GetCombinations(input, i).Where(graph.Contains));
            return combinations;
        }

        public List<string> GetCombinations(
            char[] characters, int distance, int startIndex = 0)
        {
            var combinations = new List<string>();
            for (var i = startIndex; i <= characters.Length - distance; i++)
            {
                characters[i] = Toggle(characters[i]);
                if (distance == 1)
                    combinations.Add(new string(characters));
                else
                    combinations.AddRange(GetCombinations(characters, distance - 1, i + 1));

                characters[i] = Toggle(characters[i]);
            }

            return combinations;
        }

        private char Toggle(char c)
        {
            return c == '0' ? '1' : '0';
        }

        public HashSet<string> ReadFile(string filename)
        {
            var content = new FileReader().ReadFile(filename).Skip(1).Select(x => x.TrimEnd().Replace(" ", ""));
            return content.ToHashSet();
        }

        public Dictionary<string, UnionFindNode<string>> GetInitialClusters(HashSet<string> graph)
        {
            return graph.ToDictionary(e => e, UnionFindNode<string>.Parse);
        }
    }
}