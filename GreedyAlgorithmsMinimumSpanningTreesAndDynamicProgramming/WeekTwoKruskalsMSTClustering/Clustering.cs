using System;
using System.Collections.Generic;
using System.Linq;
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
            var clusters = InitializeClusters(vertexAmount);
            var count = maxClusters;
            var space = int.MinValue;
            foreach (var edge in sortedEdges)
            {
                var node1 = clusters[edge.Vertex1];
                var node2 = clusters[edge.Vertex2];
                if (node1.IsSameUnion(node2))
                {
                    continue;
                }

                if (clusters.Count(x => x.Value.Parent == x.Value) == maxClusters)
                {
                    space = edge.Weight;
                    break;
                }
                
                node2.Union(node1);
                count--;
            }

            return space;
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
    }
}