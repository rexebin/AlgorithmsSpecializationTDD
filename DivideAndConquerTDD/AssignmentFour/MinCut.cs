using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace DivideAndConquerTDD.AssignmentFour
{
    public class MinCut
    {
        private Dictionary<int, List<int>> _graph;

        public MinCut(Dictionary<int, List<int>> graph)
        {
            _graph = graph;
        }

        public Dictionary<int, List<int>> GetGraph()
        {
            return _graph;
        }

        public static Dictionary<int, List<int>> ReadFile()
        {
            var list = new FileReader().ReadFile("AssignmentFour", "kargerMinCut.txt")
                .Select(x => x.TrimEnd().Split('\t').ToList());
            return list.ToDictionary(entry => int.Parse(entry.First()),
                entry => entry.Skip(1).Take(entry.Count).Select(x => int.Parse(x)).ToList());
        }

        public int GetMinCut()
        {
            var minCut = int.MaxValue;
            var originalGraph = CloneDictionary(_graph);
            for (int i = 0; i <= originalGraph.Count; i++)
            {
                _graph = CloneDictionary(originalGraph);
                var result = GetCutResult();
                minCut = minCut > result ? result : minCut;
            }

            return minCut;
        }

        private Dictionary<int, List<int>> CloneDictionary(Dictionary<int, List<int>> graph)
        {
            return graph.ToDictionary(entry => entry.Key,
                entry => entry.Value.ToList());
        }

        private int GetCutResult()
        {
            while (_graph.Count > 2)
            {
                var vertexToMerge = GetRandomKeyVertex();
                var targetMergeVertex = GetRandomTargetVertex(vertexToMerge);
                MergeKeyValuesToTargetValues(vertexToMerge, targetMergeVertex);
                ReplaceKeyVertexWithTargetVertex(vertexToMerge, targetMergeVertex);
                RemoveVertex(vertexToMerge);
                RemoveSelfLoop(targetMergeVertex);
            }

            return _graph.First().Value.Count;
        }

        public int GetRandomKeyVertex()
        {
            return _graph.Keys.ToList()[new Random(Guid.NewGuid().GetHashCode()).Next(0, _graph.Count)];
        }

        public int GetRandomTargetVertex(int key)
        {
            var adjacentVertices = GetValue(key);
            return adjacentVertices[new Random(Guid.NewGuid().GetHashCode()).Next(0, adjacentVertices.Count)];
        }

        public void ReplaceKeyVertexWithTargetVertex(int key, int target)
        {
            var keyVertexValues = GetValue(key);
            foreach (var keyVertexValue in keyVertexValues)
            {
                var values = GetValue(keyVertexValue);
                values = values.Select(x => x == key ? target : x).ToList();
                _graph[keyVertexValue] = values;
            }
        }

        private List<int> GetValue(int key)
        {
            _graph.TryGetValue(key, out var adjacentVertices);
            if (adjacentVertices == null) throw new Exception("key is not valid");
            return adjacentVertices;
        }

        public void RemoveVertex(int key)
        {
            _graph.Remove(key);
        }

        public void MergeKeyValuesToTargetValues(int key, int target)
        {
            var targetValues = GetValue(target);
            var keyValues = GetValue(key);
            targetValues.AddRange(keyValues);
            _graph[target] = targetValues;
        }

        public void RemoveSelfLoop(int key)
        {
            var values = GetValue(key);
            values = values.Where(x => x != key).ToList();
            _graph[key] = values;
        }
    }
}