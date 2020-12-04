using NUnit.Framework;

namespace DivideAndConquerTDD.OptionalTheoryProblemsBatchOne
{
    /**
     * You are given as input an unsorted array of n distinct numbers, where n is a power of 2.
     * Give an algorithm that identifies the second-largest number in the array, and that uses
     * at most n + log_2 n - 2 comparisons.
     */
    public class FindSecondLargestNumberTest
    {
        private FindSecondLargestNumber sut = null!;

        [SetUp]
        public void Setup()
        {
            sut = new FindSecondLargestNumber();
        }


        [Test]
        [TestCase(new[] {1, 2, 3, 4, 5, 6, 7, 9}, 7)]
        [TestCase(
            new[]
            {
                10, 3, 82, 23, 4, 5, 65, 2234, 342234, 234, 235, 2234, 2222, 4333, 44334, 344223, 7, 1123, 2123, 34,
                611, 220, 1123,
                12, 13, 14, 15, 16, 71, 18, 19, 20
            }, 342234)]
        [TestCase(
            new[]
            {
                3, 12444, 82, 23, 4, 5, 65, 2234, 342234, 234, 234, 2234, 2222, 4333, 44334, 344223, 7, 12, 2, 3, 6, 22,
                1,
                12122, 1233, 23, 31, 666, 444, 123, 456, 1254, 123, 44, 82, 23, 4, 5, 65, 2234, 342234, 234, 234, 2234,
                2222, 4333, 44335,
                344213, 7, 1113, 2, 3, 6, 22, 11233, 112, 1233, 12333, 11212, 1122, 1112, 33, 123, 123
            }, 344213)]
        public void GivenAArrayOfNumbers_ShouldReturnSecondLargestNumber(int[] numbers, int expected)
        {
            Assert.AreEqual(expected, sut.GetSecondLargestNumber(numbers));
            Assert.True(sut.IsCompareCountWithinConstraint(numbers));
        }
    }
}