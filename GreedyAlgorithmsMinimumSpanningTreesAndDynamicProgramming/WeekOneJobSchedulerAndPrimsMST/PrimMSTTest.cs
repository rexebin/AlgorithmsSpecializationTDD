using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utility.Common;
using Utility.DataStructures;
using Utility.GraphModels;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekOneJobSchedulerAndPrimsMST
{
    public class PrimMSTTest
    {
        private Dictionary<int, HashSet<Vertex>> _graph = new Dictionary<int, HashSet<Vertex>>
        {
            {1, new HashSet<Vertex> {new(2, 1), new(8, 2)}},
            {2, new HashSet<Vertex> {new(1, 1), new(3, -1), new(8,-5)}},
            {3, new HashSet<Vertex> {new(2, -1), new(4, -2)}},
            {4, new HashSet<Vertex> {new(3, -2), new(5, 1)}},
            {5, new HashSet<Vertex> {new(4, 1), new(6, 3)}},
            {6, new HashSet<Vertex> {new(5, 3), new(7, 2)}},
            {7, new HashSet<Vertex> {new(6, 2), new(8, 1)}},
            {8, new HashSet<Vertex> {new(1, 2), new (2, -5),new(7, 1), }}
        };

        [Test]
        public void GivenTestSampleFile_ShouldGenerateGraph()
        {
            var graph = PrimMST.GetGraph(ReadGraph("PrimTestSample.txt"));
            var sut = new PrimMST(graph);
            Assert.AreEqual(_graph, sut.Graph);
        }

        private static List<List<int>> ReadGraph(string filename)
        {
            return new FileReader().ReadFile("WeekOneJobSchedulerAndPrimsMST", filename)
                .Skip(1).Select(x => x.Split(" ").Select(int.Parse).ToList()).ToList();
        }

        [Test]
        public void GivenFirstProcessedVertices_ShouldReturnVerticesToProcess()
        {
            var sut = new PrimMST(_graph);
            sut.InitializeCandidates(new Vertex(1, 0));
            var candidates = new List<Vertex>
            {
                new(2, 1),
                new(8, 2),
            };
            Assert.AreEqual(candidates, sut.Candidates.ToArray());
        }
        
        [Test]
        public void GivenFirstProcessedVertices_ShouldReturnVerticesToProcess1()
        {
            var sut = new PrimMST(_graph);
            sut.InitializeCandidates(new Vertex(2, 0));
            var candidates = new List<Vertex>
            {
                new(8, -5),
                new(1, 1),
                new(3, -1),
            };
            Assert.AreEqual(candidates, sut.Candidates.ToArray());
        }

        [Test]
        public void GivenCandidates_ShouldSelectMinVertex()
        {
            var candidates = new List<Vertex>
            {
                new(2, 1),
                new(8, 2),

            };
            var sut = new PrimMST(_graph)
            {
                ProcessedVertices = new Dictionary<int, int> {{1, 0}},
                Candidates = new MinHeap<Vertex>(candidates),
            };
            var minVertex = sut.GetMinCandidate();
            Assert.AreEqual(new Vertex(2, 1), minVertex);
        }
        
        
        [Test]
        public void AddMinVertexToProcessedVertex_ShouldUpdateCandidate()
        {
            var candidates = new List<Vertex>
            {
                new(8, -5),
                new(1, 1),
                new(3, -1),
            };;
            var sut = new PrimMST(_graph)
            {
                ProcessedVertices = new Dictionary<int, int> {{2, 0}},
                Candidates = new MinHeap<Vertex>(candidates)
            };
            var minVertex = sut.GetMinCandidate();
            Assert.AreEqual(new Vertex(8, -5), minVertex);
            sut.AddMinCandidateToProcessedVertices(minVertex);
            Assert.AreEqual(new Dictionary<int, int>
            {
                {2, 0}, {8, -5}
            }, sut.ProcessedVertices);
            Assert.AreEqual(new List<Vertex>
            {
                new(3, -1),
                new(1, 1),
                new(7, 1),
            }, sut.Candidates.ToArray());
        }
        
        [Test]
        public void GivenGraph2_ShouldComputeMST()
        {
            var sut = new PrimMST(_graph);
            sut.ProcessVertices();
            Assert.AreEqual(-3, sut.GetMST());
        }
        
        [Test]
        public void GivenAssignmentGraph_ShouldComputeMST()
        {
            var sut = new PrimMST(PrimMST.GetGraph(ReadGraph("edges.txt")));
            sut.ProcessVertices();
            Assert.AreEqual(-3612829, sut.GetMST());
        }
    }
}