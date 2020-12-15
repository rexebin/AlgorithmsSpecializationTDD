using System;
using System.Collections.Generic;
using System.Linq;
using Utility.DataStructures;
using Utility.GraphModels;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.AssignmentOne
{
    public class PrimMST
    {
        public Dictionary<int, HashSet<Vertex>> Graph { get; }
        public Dictionary<int, int> ProcessedVertices { get; set; } = new();
        public MinHeap<Vertex> Candidates = new();

        public PrimMST(Dictionary<int, HashSet<Vertex>> graph)
        {
            Graph = graph;
        }

        public static Dictionary<int, HashSet<Vertex>> GetGraph(List<List<int>> inputGraph)
        {
            var groupByFirstVertex = inputGraph.GroupBy(x => x.First())
                .ToDictionary(
                    e => e.Key,
                    e => e.Select(x =>
                            new Vertex(x[1], x.Last()))
                        .ToHashSet());

            var groupBySecondVertex = inputGraph.GroupBy(x => x[1])
                .ToDictionary(
                    e => e.Key,
                    e => e.Select(x =>
                            new Vertex(x[0], x.Last()))
                        .ToHashSet());
            foreach (var v in groupBySecondVertex)
            {
                if (groupByFirstVertex.TryAdd(v.Key, v.Value)) continue;
                foreach (var x in v.Value)
                {
                    groupByFirstVertex[v.Key].Add(x);
                }
            }

            return groupByFirstVertex;
        }

        public void ProcessVertices()
        {
            ProcessedVertices = new Dictionary<int, int> {{1, 0}};
            InitializeCandidates(new Vertex(1, 0));
            while (ProcessedVertices.Count < Graph.Count)
            {
                var minCandidate = GetMinCandidate();
                AddMinCandidateToProcessedVertices(minCandidate);
            }
        }

        public void InitializeCandidates(Vertex initVertex)
        {
            Candidates = new MinHeap<Vertex>();
            Candidates.InsertMany(Graph[initVertex.Label]);
        }

        public Vertex GetMinCandidate()
        {
            var result = Candidates.Pull();
            if (result == null) throw new Exception("Min Candidate not available.");
            return result;
        }

        public void AddMinCandidateToProcessedVertices(Vertex minCandidate)
        {
            ProcessedVertices.Add(minCandidate.Label, minCandidate.Length);
            UpdateCandidate(minCandidate);
        }

        private void UpdateCandidate(Vertex minCandidate)
        {
            var adjacent = Graph[minCandidate.Label].Where(x => !ProcessedVertices.ContainsKey(x.Label));
            Candidates.TryDelete(x => adjacent.Any(v => v.Label == x.Label));
            foreach (var vertex in adjacent)
            {
                var edgesCrossing = Graph[vertex.Label]
                    .Where(x => ProcessedVertices.ContainsKey(x.Label));
                var item = new Vertex(vertex.Label, edgesCrossing.Min(x => x.Length));
                Candidates.Insert(item);
            }
        }
        
        public int GetMST()
        {
            return ProcessedVertices.Sum(x => x.Value);
        }
    }
}