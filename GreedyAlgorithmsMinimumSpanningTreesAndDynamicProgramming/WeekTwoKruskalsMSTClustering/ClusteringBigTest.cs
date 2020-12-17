using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utility.UnionFind;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekTwoKruskalsMSTClustering
{
    public class ClusteringBigTest
    {
        [Test]
        public void GivenOneBitAndDistanceOne_ShouldGetOneCombination()
        {
            var input = "1".ToCharArray();
            var sut = new ClusteringBig();
            Assert.AreEqual("0", sut.GetCombinations(input, 1).Single());
            input = "0".ToCharArray();
            Assert.AreEqual("1", sut.GetCombinations(input, 1).Single());
        }

        [Test]
        public void GivenTwoBitAndDistanceOne_ShouldReturnTwoCombinations()
        {
            var input = "00".ToCharArray();
            var sut = new ClusteringBig();
            Assert.AreEqual(new List<string> {"10", "01"}, sut.GetCombinations(input, 1));
            input = "11".ToCharArray();
            Assert.AreEqual(new List<string> {"01", "10"}, sut.GetCombinations(input, 1));
            input = "01".ToCharArray();
            Assert.AreEqual(new List<string> {"11", "00"}, sut.GetCombinations(input, 1));
            input = "10".ToCharArray();
            Assert.AreEqual(new List<string> {"00", "11"}, sut.GetCombinations(input, 1));
        }

        [Test]
        public void GivenTwoBitAndDistanceTwo_ShouldReturnTwoCombinations()
        {
            var input = "00".ToCharArray();
            var sut = new ClusteringBig();
            Assert.AreEqual(new List<string> {"11"}, sut.GetCombinations(input, 2));
            input = "11".ToCharArray();
            Assert.AreEqual(new List<string> {"00"}, sut.GetCombinations(input, 2));
            input = "01".ToCharArray();
            Assert.AreEqual(new List<string> {"10"}, sut.GetCombinations(input, 2));
            input = "10".ToCharArray();
            Assert.AreEqual(new List<string> {"01"}, sut.GetCombinations(input, 2));
        }

        [Test]
        public void GivenThreeBitAndDistanceTwo_ShouldReturnTwoCombinations()
        {
            var input = "000".ToCharArray();
            var sut = new ClusteringBig();
            Assert.AreEqual(new List<string> {"110", "101", "011"}, sut.GetCombinations(input, 2));
            input = "111".ToCharArray();
            Assert.AreEqual(new List<string> {"001", "010", "100"}, sut.GetCombinations(input, 2));
            input = "010".ToCharArray();
            Assert.AreEqual(new List<string> {"100", "111", "001"}, sut.GetCombinations(input, 2));
            input = "100".ToCharArray();
            Assert.AreEqual(new List<string> {"010", "001", "111"}, sut.GetCombinations(input, 2));
        }

        [Test]
        public void ShouldReadFile_ParseToUniqueString()
        {
            var sut = new ClusteringBig();
            var graph = sut.ReadFile("clustering_big.txt");
            Assert.AreEqual("111000001101001111001111", graph.First());
        }

        [Test]
        public void ShouldInitializeClustersPerNode()
        {
            var sut = new ClusteringBig();
            var graph = sut.ReadFile("clustering_big.txt");
            var clusters = sut.GetInitialClusters(graph);
            Assert.AreEqual(graph.Count, clusters.Count);
            Assert.AreEqual("111000001101001111001111", clusters["111000001101001111001111"].Value);
        }

        [Test]
        public void ShouldCalculateClusterQuantity()
        {
            var sut = new ClusteringBig();
            var quantity = sut.GetClusterQuantity("clustering_big.txt", 3);
            Assert.AreEqual(6118, quantity);
        }
    }
}