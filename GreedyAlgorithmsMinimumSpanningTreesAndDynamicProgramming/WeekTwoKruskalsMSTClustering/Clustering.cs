using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Utility.Common;
using Utility.UnionFind;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class Edge
    {
        public int Vertex1 { get; }
        public int Vertex2 { get; }
        public int Weight { get; }

        public Edge(int vertex1, int vertex2, int weight)
        {
            Vertex1 = vertex1;
            Vertex2 = vertex2;
            Weight = weight;
        }
    }

    public class Clustering
    {
        public static List<Edge> GetEdges(List<List<int>> graph)
        {
            return graph.Select(x => new Edge(x[0], x[1], x[2])).ToList();
        }

        public static List<List<int>> ReadFile(string filename)
        {
            return new FileReader().ReadFile("WeekTwoKruskalsMSTClustering", filename)
                .Skip(1)
                .Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
        }

        public static List<Edge> SortAscending(List<Edge> edges)
        {
            return edges.OrderBy(x => x.Weight).ToList();
        }


        public int GetSpace(List<Edge> sortedEdges, int maxClusters,
            int vertexAmount)
        {
            var lastEdgeIndex = 0;
            var result = InitializeClusters(vertexAmount);
            for (var index = 0; index < sortedEdges.Count; index++)
            {
                var edge = sortedEdges[index];
                var count = result.Count(x => x.Value.Parent == x.Value);
                if (count == maxClusters)
                {
                    lastEdgeIndex = index;
                    break;
                }

                if (result[edge.Vertex1].IsSameUnion(result[edge.Vertex2]))
                {
                    continue;
                }

                Union(result, edge);
            }

            return GetMaxSpacing(result, lastEdgeIndex, sortedEdges);
        }

        private static void Union(Dictionary<int, UnionFindNode<int>> result, Edge edge)
        {
            var fromNode = result[edge.Vertex1];
            var toNode = result[edge.Vertex2];
            toNode.Union(fromNode);
        }

        private static Dictionary<int, UnionFindNode<int>> InitializeClusters(int vertexAmount)
        {
            var result = new Dictionary<int, UnionFindNode<int>>();
            for (int i = 1; i <= vertexAmount; i++)
            {
                result.Add(i, UnionFindNode<int>.Parse(i));
            }

            return result;
        }

        public int GetMaxSpacing(Dictionary<int, UnionFindNode<int>> result, int startingIndex, List<Edge> sortedEdges)
        {
            var spacesBetweenClusters = new List<Space>();
            for (int i = startingIndex; i < sortedEdges.Count; i++)
            {
                var edge = sortedEdges[i];
                if (result[edge.Vertex1].IsSameUnion(result[edge.Vertex2]))
                {
                    continue;
                }

                var group1 = result[edge.Vertex1].Find();
                var group2 = result[edge.Vertex2].Find();
                spacesBetweenClusters.Add(new Space(group1.Value, group2.Value, edge.Weight));
            }

            return GetMaxSpacing(result, spacesBetweenClusters);
        }

        private int GetMaxSpacing(Dictionary<int, UnionFindNode<int>> result, List<Space> spacesBetweenClusters)
        {
            var clusters = result
                .Where(x => x.Value.Parent == x.Value).Select(x => x.Value)
                .ToList();
            var max = int.MaxValue;
            for (int i = 0; i < clusters.Count; i++)
            {
                int min;
                if (i != clusters.Count - 1)
                {
                    min = spacesBetweenClusters.Where(x => x.Root1 == clusters[i].Value || x.Root2 == clusters[i].Value
                        && x.Root1 == clusters[i + 1].Value || x.Root2 == clusters[i + 1].Value).Min(x => x.Weight);
                }
                else
                {
                    min = spacesBetweenClusters.Where(x => x.Root1 == clusters[i].Value || x.Root2 == clusters[i].Value
                        && x.Root1 == clusters[0].Value || x.Root2 == clusters[0].Value).Min(x => x.Weight);
                }

                max = max < min ? max : min;
            }

            return max;
        }


        public record Space(int Root1, int Root2, int Weight);
    }
}