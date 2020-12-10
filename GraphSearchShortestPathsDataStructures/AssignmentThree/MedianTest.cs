using System.Linq;
using NUnit.Framework;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentThree
{
    public class MedianTest
    {
        [Test]
        public void GivenNumbersContinuiously_ShouldReturnMedianAsExpected()
        {
            var sut = new Median();
            var median = sut.GetMedian(2793);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(6331);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(1640);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(9290);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(225);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(625);
            Assert.AreEqual(1640, median);
            median = sut.GetMedian(6195);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(2303);
            Assert.AreEqual(2303, median);
            median = sut.GetMedian(5685);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(1354);
            Assert.AreEqual(2303, median);
            median = sut.GetMedian(4292);
            Assert.AreEqual(2793, median);
            median = sut.GetMedian(1600);
            Assert.AreEqual(2303, median);
            median = sut.GetMedian(6447);
            Assert.AreEqual(2793, median);
        }

        [Test]
        public void GivenFile_ShouldReturnSumOfMedianModuloByTenThousands()
        {
            var input = new FileReader()
                .ReadFile("AssignmentThree", "Median.txt")
                .Select(int.Parse).ToArray();
            Assert.AreEqual(10000, input.Length);
            var sut = new Median();
            var result = input.Sum(x => sut.GetMedian(x)) % 10000;
            Assert.AreEqual(1213, result);
        }
    }
}