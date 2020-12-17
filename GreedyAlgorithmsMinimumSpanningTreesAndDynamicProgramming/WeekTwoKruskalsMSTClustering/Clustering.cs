using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;
using Utility.UnionFind;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class Clustering
    {
        private readonly int _maxClusterQuantity;
        public Dictionary<int, UnionFindNode<int>> Clusters { get; }
        public List<Edge> Edges { get; }
        public readonly int VertexQuantity;

        public Clustering(string filename, int maxClusterQuantity)
        {
            _maxClusterQuantity = maxClusterQuantity;
            Edges = GetSortedEdgesFromFile(filename, out VertexQuantity);
            Clusters = InitialiseClusters(VertexQuantity);
        }

        private List<Edge> GetSortedEdgesFromFile(string filename, out int vertexQuantity)
        {
            var content = new FileReader().ReadFile(filename);
            vertexQuantity = int.Parse(content.First());
            return GetEdgesSortedAscending(GetEdges(content));
        }

        private List<List<int>> GetEdges(string[] content)
        {
            return content
                .Skip(1)
                .Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
        }

        private List<Edge> GetEdgesSortedAscending(List<List<int>> graph)
        {
            return graph.Select(x => new Edge(x[0], x[1], x[2]))
                .OrderBy(x => x.Weight)
                .ToList();
        }

        private Dictionary<int, UnionFindNode<int>> InitialiseClusters(int vertexQuantity)
        {
            return Enumerable.Range(1, vertexQuantity)
                .ToDictionary(e => e, UnionFindNode<int>.Parse);
        }

        public int GetSpace()
        {
            var clusterCount = VertexQuantity;
            var clusterSpace = int.MinValue;
            foreach (var edge in Edges)
            {
                if (AreEdgeVerticesSameUnion(edge)) continue;
                if (clusterCount == _maxClusterQuantity)
                {
                    clusterSpace = edge.Weight;
                    break;
                }

                Union(edge);
                clusterCount--;
            }

            return clusterSpace;
        }

        private void Union(Edge edge)
        {
            var edgeNode1 = Clusters[edge.Vertex1];
            var edgeNode2 = Clusters[edge.Vertex2];
            edgeNode1.Union(edgeNode2);
        }

        private bool AreEdgeVerticesSameUnion(Edge edge)
        {
            var edgeNode1 = Clusters[edge.Vertex1];
            var edgeNode2 = Clusters[edge.Vertex2];
            return edgeNode1.IsSameUnion(edgeNode2);
        }
    }
}