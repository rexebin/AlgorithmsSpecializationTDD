using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;
using Utility.DataStructures;
using Utility.GraphModels;

namespace GraphSearchShortestPathsDataStructures.AssignmentTwo
{
    public class ShortestPath
    {
        private readonly Dictionary<int, List<Vertex>> _graph;
        public Dictionary<int, int> ProcessedVertices { get; set; } = new();
        public MinHeap<Vertex> Candidates = new();

        public ShortestPath(Dictionary<int, List<Vertex>> graph)
        {
            _graph = graph;
        }

        public static Dictionary<int, List<Vertex>> GetGraph(string filename)
        {
            return new FileReader().ReadFile("AssignmentTwo", filename)
                .Select(x => x.TrimEnd().Split('\t'))
                .ToDictionary(e => int.Parse(e.First()),
                    e =>
                        e.Skip(1).Select(x => x.Split(","))
                            .Select(x => new Vertex(int.Parse(x[0]), int.Parse(x[1]))).ToList()
                );
        }

        public void ProcessVertices()
        {
            ProcessedVertices = new Dictionary<int, int> {{1, 0}};
            InitializeCandidates();
            while (ProcessedVertices.Count < _graph.Count)
            {
                var minCandidate = GetMinCandidate();
                AddMinCandidateToProcessedVertices(minCandidate);
                UpdateCandidates(minCandidate);
            }
        }

        public void InitializeCandidates()
        {
            Candidates.InsertMany(ProcessedVertices.SelectMany(vertex => _graph[vertex.Key]
                    .Where(x => !ProcessedVertices.ContainsKey(x.Label))
                    .Select(x => x with {Length = x.Length + ProcessedVertices[vertex.Key]}))
                .ToList());
        }

        public Vertex GetMinCandidate()
        {
            var result = Candidates.Pull();
            if (result == null) throw new Exception("Min Candidate not available.");
            Candidates.TryDelete(x => x.Label == result.Label);
            return result;
        }

        public void AddMinCandidateToProcessedVertices(Vertex minCandidate)
        {
            ProcessedVertices.Add(minCandidate.Label, minCandidate.Length);
        }

        public void UpdateCandidates(Vertex minCandidate)
        {
            var (label, _) = minCandidate;
            Candidates.InsertMany(_graph[label]
                .Where(x => !ProcessedVertices.ContainsKey(x.Label))
                .Select(v => v with{Length = v.Length + ProcessedVertices[label]}));
        }
    }
}