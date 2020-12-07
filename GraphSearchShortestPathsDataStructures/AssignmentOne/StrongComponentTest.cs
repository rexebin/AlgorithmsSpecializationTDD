using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using static System.Int32;

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

        private StrongComponents _sut = null!;

        [SetUp]
        public void Setup()
        {
            _sut = new StrongComponents();
        }

        [Test]
        public void GivenEdges_ShouldGetGraph()
        {
            var result = StrongComponents.GetGraph(_testGraph);
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
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenEdges_ShouldGetReversedGraph()
        {
            var result = StrongComponents.GetReversedGraph(_testGraph);
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
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenGraph_AfterFirstReverseSearch_ShouldMarkFinishingTime()
        {
            var reversedGraph = StrongComponents.GetReversedGraph(_testGraph);
            _sut.DepthFirstSearch(reversedGraph);
            Assert.AreEqual(1, _sut.FinishingTimes[3]);
            Assert.AreEqual(2, _sut.FinishingTimes[5]);
            Assert.AreEqual(3, _sut.FinishingTimes[2]);
            Assert.AreEqual(4, _sut.FinishingTimes[8]);
            Assert.AreEqual(5, _sut.FinishingTimes[6]);
            Assert.AreEqual(6, _sut.FinishingTimes[9]);

            Assert.AreEqual(7, _sut.FinishingTimes[1]);
            Assert.AreEqual(8, _sut.FinishingTimes[4]);
            Assert.AreEqual(9, _sut.FinishingTimes[7]);
        }

        [Test]
        public void GivenGraph_AfterFirstReverseSearch_ShouldReplaceVertexWithFinishingTime()
        {
            var reversedGraph = StrongComponents.GetReversedGraph(_testGraph);
            _sut.DepthFirstSearch(reversedGraph);
            var graph = StrongComponents.GetGraph(_testGraph);
            var newGraph = _sut.ReplaceVertexWithFinishTime(graph);
            Assert.AreEqual(new[] {8}, newGraph[7]);
            Assert.AreEqual(new[] {4}, newGraph[3]);
            Assert.AreEqual(new[] {5}, newGraph[1]);
            Assert.AreEqual(new[] {9}, newGraph[8]);
            Assert.AreEqual(new[] {3}, newGraph[2]);
            Assert.AreEqual(new[] {6}, newGraph[5]);
            Assert.AreEqual(new[] {7}, newGraph[9]);
            Assert.AreEqual(new[] {5, 2}, newGraph[4]);
            Assert.AreEqual(new[] {1, 9}, newGraph[6]);
        }


        [Test]
        public void GivenGraph_AfterBothSearch_ShouldSetLeaders_And_GetSCCount()
        {
            var reversedGraph = StrongComponents.GetReversedGraph(_testGraph);
            _sut.DepthFirstSearch(reversedGraph);
            var graph = StrongComponents.GetGraph(_testGraph);
            var newGraph = _sut.ReplaceVertexWithFinishTime(graph);
            _sut.DepthFirstSearch(newGraph);
            Assert.AreEqual(9, _sut.Leads[9]);
            Assert.AreEqual(9, _sut.Leads[8]);
            Assert.AreEqual(9, _sut.Leads[7]);

            Assert.AreEqual(6, _sut.Leads[6]);
            Assert.AreEqual(6, _sut.Leads[5]);
            Assert.AreEqual(6, _sut.Leads[1]);

            Assert.AreEqual(4, _sut.Leads[4]);
            Assert.AreEqual(4, _sut.Leads[2]);
            Assert.AreEqual(4, _sut.Leads[3]);

            var strongComponentsCounts = _sut.GetFiveStrongComponentsCounts();
            Assert.AreEqual(new[] {3, 3, 3}, strongComponentsCounts);
        }

        [Test]
        public void GivenSCCFile_ShouldReadAndSearchAndGetFiveLargestSCCounts()
        {
            var thread = new Thread(() =>
            {
                var fileInput = StrongComponents.ReadFile();
                var sut = new StrongComponents();
                var result = sut.GetStrongComponents(fileInput);
                Assert.AreEqual(new[] {434821, 968, 459, 313, 211}, result);
            }, MaxValue);
            thread.Start();
            thread.Join();
        }
    }
}