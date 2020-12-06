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
            // var graph = StrongComponents.TransformToGraph(input);
            _sut = new StrongComponents();
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

        //
        // [Test]
        // public void GivenArrayShouldReturnGraph()
        // {
        //     var array = new[]
        //     {
        //         new[] {1, 4},
        //         new[] {2, 8},
        //         new[] {3, 6},
        //         new[] {4, 7},
        //         new[] {5, 2},
        //         new[] {6, 9},
        //         new[] {7, 1},
        //         new[] {8, 6},
        //         new[] {8, 5},
        //         new[] {9, 3},
        //         new[] {9, 7}
        //     };
        //
        //     var graph = StrongComponents.TransformToGraph(array);
        //
        //     Assert.AreEqual(7, graph[1].Tails.First().OriginalNo);
        //     Assert.AreEqual(4, graph[1].Heads.First().OriginalNo);
        //
        //     Assert.AreEqual(8, graph[2].Heads.First().OriginalNo);
        //     Assert.AreEqual(5, graph[2].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(6, graph[3].Heads.First().OriginalNo);
        //     Assert.AreEqual(9, graph[3].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(7, graph[4].Heads.First().OriginalNo);
        //     Assert.AreEqual(1, graph[4].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(2, graph[5].Heads.First().OriginalNo);
        //     Assert.AreEqual(8, graph[5].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(9, graph[6].Heads.First().OriginalNo);
        //     Assert.AreEqual(3, graph[6].Tails.First().OriginalNo);
        //     Assert.AreEqual(8, graph[6].Tails.Last().OriginalNo);
        //
        //     Assert.AreEqual(1, graph[7].Heads.First().OriginalNo);
        //     Assert.AreEqual(4, graph[7].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(6, graph[8].Heads.First().OriginalNo);
        //     Assert.AreEqual(5, graph[8].Heads.Last().OriginalNo);
        //     Assert.AreEqual(2, graph[8].Tails.First().OriginalNo);
        //
        //     Assert.AreEqual(3, graph[9].Heads.First().OriginalNo);
        //     Assert.AreEqual(7, graph[9].Heads.Last().OriginalNo);
        //     Assert.AreEqual(6, graph[9].Tails.First().OriginalNo);
        //
        //     foreach (var vertex in graph)
        //     {
        //         Assert.False(vertex.Value.IsVisited);
        //         Assert.Null(vertex.Value.Leader);
        //         Assert.AreEqual(0, vertex.Value.FinishTime);
        //         Assert.AreEqual(vertex.Key, vertex.Value.OriginalNo);
        //     }
        // }
        //
        [Test]
        public void GivenGroupAndLeadVertex_ShouldMarkFinishingTime()
        {
            var reversedGraph = StrongComponents.GroupByHeads(input);
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

            // Assert.AreEqual(9, graph[3].Leader?.OriginalNo);
            // Assert.AreEqual(9, graph[5].Leader?.OriginalNo);
            // Assert.AreEqual(9, graph[2].Leader?.OriginalNo);
            // Assert.AreEqual(9, graph[8].Leader?.OriginalNo);
            // Assert.AreEqual(9, graph[6].Leader?.OriginalNo);
            // Assert.AreEqual(9, graph[9].Leader?.OriginalNo);
        }

        [Test]
        public void GivenFirstReverseSearchResult_ShouldSetFinishTimeToKey()
        {
            var reversedGraph = StrongComponents.GroupByHeads(input);
            _sut.DepthFirstSearch(reversedGraph);
            var graph = StrongComponents.GroupByTails(input);
            var newGraph = _sut.FinishTimeToKeyAndResetStatus(graph);
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
        public void SearchProcessedGraphInCorrectOrder_ShouldSetLeaders()
        {
            var reversedGraph = StrongComponents.GroupByHeads(input);
            _sut.DepthFirstSearch(reversedGraph);
            var graph = StrongComponents.GroupByTails(input);
            var newGraph = _sut.FinishTimeToKeyAndResetStatus(graph);
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

            var strongComponentsCounts = _sut.GetStrongComponentsCounts();
            Assert.AreEqual(new[] {3, 3, 3}, strongComponentsCounts);
        }

        [Test]
        public void ShouldReadSCC_ReturnGraph()
        {
            var fileInput = StrongComponents.ReadFile();
            // var reversedGraph = StrongComponents.GroupByHeads(fileInput);
            // _sut.DepthFirstSearch(reversedGraph);
            // var graph = StrongComponents.GroupByTails(fileInput);
            // var newGraph = _sut.FinishTimeToKeyAndResetStatus(graph);
            // _sut.DepthFirstSearch(newGraph);
            // Assert.AreEqual(875714, graph.Count);
            // _sut = new StrongComponents(graph);
            // _sut.DepthFirstSearch(true);
            // graph = _sut.GetGraph();
            // // Assert.AreEqual(875714, graph.Count);
            // _sut.FinishTimeToKeyAndResetStatus();
            // // graph = _sut.GetGraph();
            // // Assert.AreEqual(875714, graph.Count);
            // _sut.DepthFirstSearch(false);
            // // // graph = _sut.GetGraph();
            // // // Assert.AreEqual(875714, graph.Count);
            _sut.GetStrongComponents(fileInput);
            // var result = _sut.GetStrongComponentsCounts();
            // Assert.AreEqual(new[]{1}, result);
        }
    }
}