using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class ClusteringTest
    {
        private List<List<int>> _graph = new();

        [SetUp]
        public void ShouldReadFile()
        {
            _graph = Clustering.ReadFile("clustering1.txt");
            Assert.AreEqual(124750, _graph.Count);
            Assert.AreEqual(new List<int> {1, 2, 6808}, _graph.First());
            Assert.AreEqual(new List<int> {499, 500, 8273}, _graph.Last());
        }

        [Test]
        public void GivenListOfGraph_ShouldTransformToListOfEdge()
        {
            var edges = Clustering.GetEdges(_graph);
            Assert.AreEqual(124750, edges.Count);
            Assert.AreEqual(1, edges.First().Vertex1);
            Assert.AreEqual(2, edges.First().Vertex2);
            Assert.AreEqual(6808, edges.First().Weight);
            Assert.AreEqual(499, edges.Last().Vertex1);
            Assert.AreEqual(500, edges.Last().Vertex2);
            Assert.AreEqual(8273, edges.Last().Weight);
        }

        [Test]
        public void GivenListOfEdges_ShouldReturnSortByWeightAscending()
        {
            var sortedEdge = Clustering.SortAscending(Clustering.GetEdges(_graph));
            Assert.AreEqual(124750, sortedEdge.Count);
            Assert.AreEqual(1, sortedEdge.First().Weight);
            Assert.AreEqual(10000, sortedEdge.Last().Weight);
        }

        [Test]
        public void GivenTestSample_ShouldReturnFourClusters()
        {
            
        }

        [Test]
        public void GivenSortedUnionFindNodes_ShouldToThroughEachNodeAscendingUntilFourLeft()
        {
            var testGraph = Clustering.ReadFile("clusterTest.txt");
            var graph = Clustering.SortAscending(
                Clustering.GetEdges(testGraph));
            var sut = new Clustering();
            var space = sut.GetSpace(graph,3 ,12);
            Assert.AreEqual(10, space);
        }
        
        [Test]
        public void Given500SortedUnionFindNodes_ShouldToThroughEachNodeAscendingUntilFourLeft()
        {
            var graph = Clustering.SortAscending(
                Clustering.GetEdges(_graph));
            var sut = new Clustering();
            var space = sut.GetSpace(graph,4 ,500);
            Assert.AreEqual(106, space);
        }
    }
}