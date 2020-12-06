using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class StrongComponentTest
    {
        private int[][] input =
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
            var graph = StrongComponents.TransformToGraph(input);
            _sut = new StrongComponents(graph);
        }

        [Test]
        public void GivenArrayShouldGroupByFirstNumber()
        {
            var array = new[]
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

            var result = StrongComponents.GroupByTails(array);
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
        public void GivenArrayShouldGroupByLastNumber()
        {
            var array = new[]
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

            var result = StrongComponents.GroupByHeads(array);
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
        public void GivenArrayShouldReturnGraph()
        {
            var array = new[]
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

            var graph = StrongComponents.TransformToGraph(array);

            Assert.AreEqual(7, graph[1].Tails.First().OriginalNo);
            Assert.AreEqual(4, graph[1].Heads.First().OriginalNo);

            Assert.AreEqual(8, graph[2].Heads.First().OriginalNo);
            Assert.AreEqual(5, graph[2].Tails.First().OriginalNo);

            Assert.AreEqual(6, graph[3].Heads.First().OriginalNo);
            Assert.AreEqual(9, graph[3].Tails.First().OriginalNo);

            Assert.AreEqual(7, graph[4].Heads.First().OriginalNo);
            Assert.AreEqual(1, graph[4].Tails.First().OriginalNo);

            Assert.AreEqual(2, graph[5].Heads.First().OriginalNo);
            Assert.AreEqual(8, graph[5].Tails.First().OriginalNo);

            Assert.AreEqual(9, graph[6].Heads.First().OriginalNo);
            Assert.AreEqual(3, graph[6].Tails.First().OriginalNo);
            Assert.AreEqual(8, graph[6].Tails.Last().OriginalNo);

            Assert.AreEqual(1, graph[7].Heads.First().OriginalNo);
            Assert.AreEqual(4, graph[7].Tails.First().OriginalNo);

            Assert.AreEqual(6, graph[8].Heads.First().OriginalNo);
            Assert.AreEqual(5, graph[8].Heads.Last().OriginalNo);
            Assert.AreEqual(2, graph[8].Tails.First().OriginalNo);

            Assert.AreEqual(3, graph[9].Heads.First().OriginalNo);
            Assert.AreEqual(7, graph[9].Heads.Last().OriginalNo);
            Assert.AreEqual(6, graph[9].Tails.First().OriginalNo);

            foreach (var vertex in graph)
            {
                Assert.False(vertex.Value.IsVisited);
                Assert.Null(vertex.Value.Leader);
                Assert.AreEqual(0, vertex.Value.FinishTime);
                Assert.AreEqual(vertex.Key, vertex.Value.OriginalNo);
            }
        }

        [Test]
        public void GivenGroupAndLeadVertex_ShouldMarkFinishingTime()
        {
            _sut.DepthFirstSearch(true);
            var graph = _sut.GetGraph();
            Assert.AreEqual(1, graph[3].FinishTime);
            Assert.AreEqual(2, graph[5].FinishTime);
            Assert.AreEqual(3, graph[2].FinishTime);
            Assert.AreEqual(4, graph[8].FinishTime);
            Assert.AreEqual(5, graph[6].FinishTime);
            Assert.AreEqual(6, graph[9].FinishTime);

            Assert.AreEqual(7, graph[1].FinishTime);
            Assert.AreEqual(8, graph[4].FinishTime);
            Assert.AreEqual(9, graph[7].FinishTime);

            Assert.AreEqual(9, graph[3].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[5].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[2].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[8].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[6].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[9].Leader?.OriginalNo);
        }

        [Test]
        public void GivenFirstReverseSearchResult_ShouldSetFinishTimeToKey()
        {
            _sut.DepthFirstSearch(true);
            _sut.FinishTimeToKeyAndResetStatus();
            var graph = _sut.GetGraph();
            Assert.AreEqual(1, graph[7].OriginalNo);
            Assert.AreEqual(2, graph[3].OriginalNo);
            Assert.AreEqual(3, graph[1].OriginalNo);
            Assert.AreEqual(4, graph[8].OriginalNo);
            Assert.AreEqual(5, graph[2].OriginalNo);
            Assert.AreEqual(6, graph[5].OriginalNo);
            Assert.AreEqual(7, graph[9].OriginalNo);
            Assert.AreEqual(8, graph[4].OriginalNo);
            Assert.AreEqual(9, graph[6].OriginalNo);

            Assert.False(graph[1].IsVisited);
            Assert.False(graph[2].IsVisited);
            Assert.False(graph[3].IsVisited);
            Assert.False(graph[4].IsVisited);
            Assert.False(graph[5].IsVisited);
            Assert.False(graph[6].IsVisited);
            Assert.False(graph[7].IsVisited);
            Assert.False(graph[8].IsVisited);
            Assert.False(graph[9].IsVisited);
        }

        [Test]
        public void SearchProcessedGraphInCorrectOrder_ShouldSetLeaders()
        {
            _sut.DepthFirstSearch(true);
            _sut.FinishTimeToKeyAndResetStatus();
            _sut.DepthFirstSearch(false);
            var graph = _sut.GetGraph();
            Assert.AreEqual(7, graph[9].Leader?.OriginalNo);
            Assert.AreEqual(7, graph[8].Leader?.OriginalNo);
            Assert.AreEqual(7, graph[7].Leader?.OriginalNo);

            Assert.AreEqual(9, graph[6].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[5].Leader?.OriginalNo);
            Assert.AreEqual(9, graph[1].Leader?.OriginalNo);

            Assert.AreEqual(8, graph[4].Leader?.OriginalNo);
            Assert.AreEqual(8, graph[2].Leader?.OriginalNo);
            Assert.AreEqual(8, graph[3].Leader?.OriginalNo);

            var strongComponentsCounts = _sut.GetStrongComponentsCounts();
            Assert.AreEqual(new[] {3, 3, 3}, strongComponentsCounts);
        }

        [Test]
        public void ShouldReadSCC_ReturnGraph()
        {
            var graph = StrongComponents.ReadFile();
            // Assert.AreEqual(875714, graph.Count);
            _sut = new StrongComponents(graph);
            _sut.DepthFirstSearch(true);
            // graph = _sut.GetGraph();
            // Assert.AreEqual(875714, graph.Count);
            _sut.FinishTimeToKeyAndResetStatus();
            // graph = _sut.GetGraph();
            // Assert.AreEqual(875714, graph.Count);
            _sut.DepthFirstSearch(false);
            // graph = _sut.GetGraph();
            // Assert.AreEqual(875714, graph.Count);
            var result = _sut.GetStrongComponentsCounts();
            Assert.AreEqual(new[]{1}, result);
        }
    }
}