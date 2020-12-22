using System.Collections.Generic;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class MwisTest
    {
        [Test]
        public void ShouldGoThroughVertices_AndMemorizeMaxWeight()
        {
            var sut = new Mwis
                (new List<int> {1, 4, 1, 1, 3, 7, 8, 9, 9, 5, 4, 2});
            var expected = new List<int> {0, 1, 4, 4, 5, 7, 12, 15, 21, 24, 26, 28, 28};
            // 28 = 2+5+9+7+1+4
            // 28 = 4+9+8+3+4
            Assert.AreEqual(expected, sut.GetMaxWeights());
        }

        [Test]
        public void Should_GetMaxIndependentWeights()
        {
            var sut = new Mwis
                (new List<int> {1, 4, 1, 1, 3, 7, 8, 9, 9, 5, 4, 2});
            var result = sut.GetMaxIndependentWeight();
            var expected = new Dictionary<int, int>
            {
                {11, 4}, {9, 9}, {7, 8}, {5, 3}, {2, 4}
            };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldReadFileToArray()
        {
            var sut = new Mwis(ReadFile());
            Assert.AreEqual(1000, sut.Weights.Count);
        }

        private static List<int> ReadFile()
        {
            return Mwis.ReadFile("Mwis.txt");
        }

        [Test]
        public void ShouldReturnAnswer()
        {
            var sut = new Mwis(ReadFile());
            var answer = sut.GetAnswer();
            var expected = "10100110";
            Assert.AreEqual(expected, answer);
        }
    }
}