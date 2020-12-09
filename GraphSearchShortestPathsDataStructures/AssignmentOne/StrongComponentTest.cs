using System.Collections.Generic;
using NUnit.Framework;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class StrongComponentTest
    {
        private readonly int[][] _testGraph =
        {
            new[] {1, 4},
            new[] {2, 8},
            new[] {3, 6},
            new[] {4, 7},
            new[] {5, 2},
            new[] {6, 9},
            new[] {7, 1},
            new[] {8, 6},
            new[] {8, 5},
            new[] {9, 3},
            new[] {9, 7}
        };

        [Test]
        public void GivenEdges_ShouldGetGraph()
        {
            var graph = GraphReader.GetGraph(_testGraph);
            var expected = new Dictionary<int, int[]>()
            {
                {1, new[] {4}},
                {2, new[] {8}},
                {3, new[] {6}},
                {4, new[] {7}},
                {5, new[] {2}},
                {6, new[] {9}},
                {7, new[] {1}},
                {8, new[] {6, 5}},
                {9, new[] {3, 7}},
            };
            Assert.AreEqual(expected, graph);
        }

        [Test]
        public void GivenEdges_ShouldGetReversedGraph()
        {
            var graph = GraphReader.GetReversedGraph(_testGraph);
            var expected = new Dictionary<int, int[]>()
            {
                {1, new[] {7}},
                {2, new[] {5}},
                {3, new[] {9}},
                {4, new[] {1}},
                {5, new[] {8}},
                {6, new[] {3, 8}},
                {7, new[] {4, 9}},
                {8, new[] {2}},
                {9, new[] {6}},
            };
            Assert.AreEqual(expected, graph);
        }

        [Test]
        public void GivenGraph_AfterFirstReverseSearch_ShouldMarkFinishingTime()
        {
            var sut = new StrongComponents();
            sut.SearchGraph(GraphReader.GetReversedGraph(_testGraph));
            Assert.AreEqual(4, sut.FinishingTimeTracker[3]);
            Assert.AreEqual(1, sut.FinishingTimeTracker[5]);
            Assert.AreEqual(2, sut.FinishingTimeTracker[2]);
            Assert.AreEqual(3, sut.FinishingTimeTracker[8]);
            Assert.AreEqual(5, sut.FinishingTimeTracker[6]);
            Assert.AreEqual(6, sut.FinishingTimeTracker[9]);

            Assert.AreEqual(7, sut.FinishingTimeTracker[1]);
            Assert.AreEqual(8, sut.FinishingTimeTracker[4]);
            Assert.AreEqual(9, sut.FinishingTimeTracker[7]);
        }

        [Test]
        public void GivenGraph_AfterFirstReverseSearch_ShouldReplaceVertexWithFinishingTime()
        {
            var sut = new StrongComponents();
            var graph = GraphReader.GetGraph(_testGraph);
            var reversedGraph = GraphReader.GetReversedGraph(_testGraph);
            sut.SearchGraph(reversedGraph);
            graph = sut.ReplaceVertexWithFinishTime(graph);
            Assert.AreEqual(new[] {8}, graph[7]);
            Assert.AreEqual(new[] {5, 1}, graph[3]);
            Assert.AreEqual(new[] {2}, graph[1]);
            Assert.AreEqual(new[] {9}, graph[8]);
            Assert.AreEqual(new[] {3}, graph[2]);
            Assert.AreEqual(new[] {6}, graph[5]);
            Assert.AreEqual(new[] {7}, graph[9]);
            Assert.AreEqual(new[] {5}, graph[4]);
            Assert.AreEqual(new[] {4, 9}, graph[6]);
        }


        [Test]
        public void GivenGraph_AfterBothSearch_ShouldSetLeaders_And_GetSCCount()
        {
            var sut = new StrongComponents();
            var graph = GraphReader.GetGraph(_testGraph);
            var reversedGraph = GraphReader.GetReversedGraph(_testGraph);
            var result = sut.GetStrongComponents(graph, reversedGraph);
            Assert.AreEqual(9, sut.LeadTracker[9]);
            Assert.AreEqual(9, sut.LeadTracker[8]);
            Assert.AreEqual(9, sut.LeadTracker[7]);

            Assert.AreEqual(6, sut.LeadTracker[6]);
            Assert.AreEqual(6, sut.LeadTracker[5]);
            Assert.AreEqual(6, sut.LeadTracker[4]);

            Assert.AreEqual(3, sut.LeadTracker[1]);
            Assert.AreEqual(3, sut.LeadTracker[2]);
            Assert.AreEqual(3, sut.LeadTracker[3]);

            Assert.AreEqual(new[] {3, 3, 3}, result);
        }

        [Test]
        public void GivenSCCFile_ShouldReadAndSearchAndGetFiveLargestSCCounts()
        {
            var fileInput = GraphReader.ReadFile("SCC.txt");
            var sut = new StrongComponents();
            var graph = GraphReader.AddMissingVertices(GraphReader.GetGraph(fileInput));
            var reversedGraph = GraphReader.AddMissingVertices(GraphReader.GetReversedGraph(fileInput));
            var result = sut.GetStrongComponents(graph, reversedGraph);
            Assert.AreEqual(new[] {434821, 968, 459, 313, 211}, result);
        }
    }
}