using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GraphSearchShortestPathsDataStructures.AssignmentTwo
{
    public class ShortestPathTest
    {
        private readonly Dictionary<int, List<Vertex>> _testGraph = new()
        {
            {1, new List<Vertex> {new(2, 1), new(8, 2)}},
            {2, new List<Vertex> {new(1, 1), new(3, 1)}},
            {3, new List<Vertex> {new(2, 1), new(4, 1)}},
            {4, new List<Vertex> {new(3, 1), new(5, 1)}},
            {5, new List<Vertex> {new(4, 1), new(6, 2)}},
            {6, new List<Vertex> {new(5, 1), new(7, 3)}},
            {7, new List<Vertex> {new(6, 1), new(8, 1)}},
            {8, new List<Vertex> {new(7, 1), new(1, 2)}}
        };

        [Test]
        public void GivenFile_ShouldReturnGraph()
        {
            var result = ShortestPath.GetGraph("TestSample.txt");
            Assert.AreEqual(_testGraph, result);
        }

        [Test]
        public void GivenFirstProcessedVertices_ShouldReturnVerticesToProcess()
        {
            var sut = new ShortestPath(_testGraph) {ProcessedVertices = new Dictionary<int, int> {{1, 0}}};
            sut.InitializeCandidates();
            Assert.AreEqual(new List<Candidate>
            {
                new(new(2, 1), 1),
                new(new(8, 2), 1),
            }, sut.Candidates);
        }

        [Test]
        public void GivenCandidates_ShouldSelectMinVertex()
        {
            var sut = new ShortestPath(_testGraph)
            {
                ProcessedVertices = new Dictionary<int, int> {{1, 0}},
                Candidates = new List<Candidate> {new(new(2, 1), 1), new(new(8, 2), 1),}
            };
            var minVertex = sut.GetMinCandidate();
            Assert.AreEqual(new Candidate(new Vertex(2, 1), 1), minVertex);
        }

        [Test]
        public void GivenProcessedVertices_ShouldReturnVerticesToProcess()
        {
            var sut = new ShortestPath(_testGraph) {ProcessedVertices = new Dictionary<int, int> {{1, 0}, {2, 1}}};
            sut.InitializeCandidates();
            Assert.AreEqual(new List<Candidate>
            {
                new(new(8, 2), 1),
                new(new(3, 1), 2),
            }, sut.Candidates);
        }

        [Test]
        public void GivenCandidates_ShouldSelectMinVertex1()
        {
            var sut = new ShortestPath(_testGraph)
            {
                ProcessedVertices = new Dictionary<int, int> {{1, 0}, {2, 1}},
                Candidates = new List<Candidate> {new(new(8, 2), 1), new(new(3, 1), 2),}
            };
            var minVertex = sut.GetMinCandidate();
            Assert.AreEqual(new Candidate(new Vertex(8, 2), 1), minVertex);
        }

        [Test]
        public void AddMinVertexToProcessedVertex_ShouldUpdateCandidate()
        {
            var sut = new ShortestPath(_testGraph)
            {
                ProcessedVertices = new Dictionary<int, int> {{1, 0}, {2, 1}},
                Candidates = new List<Candidate> {new(new(8, 2), 1), new(new(3, 1), 2),}
            };
            var minVertex = sut.GetMinCandidate();
            sut.AddMinCandidateToProcessedVertices(minVertex);
            Assert.AreEqual(new Dictionary<int, int>
            {
                {1, 0}, {2, 1}, {8, 2}
            }, sut.ProcessedVertices);
            sut.InitializeCandidates();
            Assert.AreEqual(new List<Candidate>
            {
                new(new(3, 1), 2),
                new(new(7, 1), 8),
            }, sut.Candidates);
        }


        [Test]
        public void GivenGraph2_ShouldComputeShortestPath()
        {
            var sut = new ShortestPath(_testGraph);
            sut.ProcessVertices();
            Assert.AreEqual(new Dictionary<int, int>
            {
                {1, 0}, {2, 1}, {3, 2}, {4, 3}, {5, 4}, {6, 4}, {7, 3},
                {
                    8, 2
                }
            }, sut.ProcessedVertices);
        }

        [Test]
        public void GivenAssignmentGraph_ShouldComputeShortestPath()
        {
            var graph = ShortestPath.GetGraph("dijkstraData.txt");
            var sut = new ShortestPath(graph);
            sut.ProcessVertices();
            var vertices = new[] {7, 37, 59, 82, 99, 115, 133, 165, 188, 197};
            var shortestPath = new[] {2599, 2610, 2947, 2052, 2367, 2399, 2029, 2442, 2505, 3068};
            Assert.AreEqual(shortestPath, vertices.Select(v => sut.ProcessedVertices[v]));
        }
    }
}