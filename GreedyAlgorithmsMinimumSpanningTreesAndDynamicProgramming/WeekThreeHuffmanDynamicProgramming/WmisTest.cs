using System.Collections.Generic;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class WmisTest
    {
        [Test]
        public void ShouldGoThroughVertices_AndMemorizeMaxWeight()
        {
            var sut = new Wmis();
            var weights = new[] {1, 4, 5, 4, 3, 7, 8, 9, 10, 5, 4, 2};
            var expected = new[] {0, 1, 4, 6, 8, 9, 15, 17, 24, 27, 29, 31, 31};
            Assert.AreEqual(expected, sut.GetMaxWeights(weights));
        }

        [Test]
        public void Should_GetMaxIndependentWeights()
        {
            var sut = new Wmis();
            var weights = new[] {1, 4, 1, 1, 3, 7, 8, 9, 9, 5, 4, 2};
            var maxWeights = sut.GetMaxWeights(weights);
            var result = sut.GetMaxIndependentWeight(maxWeights, weights);
            var expected = new Dictionary<int, int>
            {
                {12, 2}, {10, 5}, {8, 9}
            };
            Assert.AreEqual(expected, result);
        }
    }
}