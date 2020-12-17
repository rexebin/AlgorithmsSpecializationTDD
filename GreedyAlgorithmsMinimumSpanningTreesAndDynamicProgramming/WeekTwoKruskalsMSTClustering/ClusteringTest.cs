using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class ClusteringTest
    {
        private Clustering _sut = null!;

        [SetUp]
        public void ShouldReadFile()
        {
            _sut = new Clustering("clusterTest.txt", 3);
        }

        [Test]
        public void ShouldInitializeSortedByWeightEdges()
        {
            Assert.AreEqual(18, _sut.Edges.Count);
            Assert.AreEqual(2, _sut.Edges.First().Vertex1);
            Assert.AreEqual(4, _sut.Edges.First().Vertex2);
            Assert.AreEqual(1, _sut.Edges.First().Weight);
            Assert.AreEqual(7, _sut.Edges.Last().Vertex1);
            Assert.AreEqual(10, _sut.Edges.Last().Vertex2);
            Assert.AreEqual(20, _sut.Edges.Last().Weight);
        }
        
        [Test]
        public void ShouldInitializeClusters()
        {
            Assert.AreEqual(12, _sut.Clusters.Count);
        }
        
        [Test]
        public void ShouldInitializeVertexQuantity()
        {
            Assert.AreEqual(12, _sut.VertexQuantity);
        }

        [Test]
        public void GivenTestSample_ShouldToThroughEachNodeAscendingUntilThreeLeft()
        {
            var space = _sut.GetSpace();
            Assert.AreEqual(10, space);
        }

        [Test]
        public void Given500SortedUnionFindNodes_ShouldToThroughEachNodeAscendingUntilFourLeft()
        {
            var sut = new Clustering("clustering1.txt", 4);
            Assert.AreEqual(106, sut.GetSpace());
        }
    }
}