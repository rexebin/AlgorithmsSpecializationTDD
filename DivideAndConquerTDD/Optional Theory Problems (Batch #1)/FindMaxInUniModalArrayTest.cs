using NUnit.Framework;

namespace DivideAndConquerTDD
{
    public class FindMaxInUniModalArrayTest
    {
        private FindMaxInUniModalArray _sut = null!;

        [SetUp]
        public void Setup()
        {
            _sut = new FindMaxInUniModalArray();
        }

        [Test]
        [TestCase(new[] {1, 3, 4, 5, 3, 2, 1}, new[] {5, 3, 2, 1})]
        [TestCase(new[] {1, 3, 4, 5, 50, 40, 30, 3, 2, 1}, new[] {1, 3, 4, 5, 50})]
        [TestCase(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9, 8, 7, 6, 5, 4}, new[] {9, 10, 9, 8, 7, 6, 5, 4})]
        public void GivenAnArray_ShouldReturnPeakHalf(int[] numbers, int[] expected)
        {
            Assert.AreEqual(expected, _sut.GetPeakPart(numbers));
        }

        [Test]
        [TestCase(new[] {1, 3, 4, 5, 3, 2, 1}, 5)]
        [TestCase(new[] {1, 3, 4, 5, 50, 40, 30, 3, 2, 1}, 50)]
        [TestCase(new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 9, 8, 7, 6, 5, 4}, 10)]
        public void GivenAnArray_ShouldReturnMax(int[] numbers, int expected)
        {
            Assert.AreEqual(expected, _sut.GetMax(numbers));
        }
    }
}