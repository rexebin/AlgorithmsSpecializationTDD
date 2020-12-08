using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentTwo
{
    public record Candidate(Vertex Vertex, int Parent);

    public record Vertex(int Label, int Length);

    public class ShortestPath
    {
        private readonly Dictionary<int, List<Vertex>> _graph;
        public Dictionary<int, int> ProcessedVertices { get; set; } = new();
        public List<Candidate> Candidates = new();

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
            Candidates = ProcessedVertices.SelectMany(vertex => _graph[vertex.Key]
                .Where(x => !ProcessedVertices.ContainsKey(x.Label))
                .Select(x => new Candidate(x, vertex.Key))).ToList();
        }

        public Candidate GetMinCandidate()
        {
            return Candidates.Aggregate((acc, current) =>
                acc.Vertex.Length + ProcessedVertices[acc.Parent] >
                current.Vertex.Length + ProcessedVertices[current.Parent]
                    ? current
                    : acc
            );
        }

        public void AddMinCandidateToProcessedVertices(Candidate minCandidate)
        {
            var ((label, length), parent) = minCandidate;
            ProcessedVertices.Add(label,
                length + ProcessedVertices[parent]);
        }

        private void UpdateCandidates(Candidate minCandidate)
        {
            var ((label, _), _) = minCandidate;
            Candidates = Candidates.Where(x => x.Vertex.Label != label).ToList();
            Candidates.AddRange(_graph[label]
                .Where(x => !ProcessedVertices.ContainsKey(x.Label))
                .Select(v => new Candidate(v, label)));
        }
    }
}