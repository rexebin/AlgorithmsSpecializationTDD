using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DivideAndConquerTDD
{
    /**
     * You are given an n by n grid of distinct numbers. A number is a local minimum if it is smaller than all of its
     * neighbors. (A neighbor of a number is one immediately above, below, to the left, or the right. Most numbers have
     * four neighbors; numbers on the side have three; the four corners have two.) Use the divide-and-conquer algorithm
     * design paradigm to compute a local minimum with only O(n) comparisons between pairs of numbers. (Note: since
     * there are n^2 numbers in the input, you cannot afford to look at all of them. Hint: Think about what types
     * of recurrences would give you the desired upper bound.)
     */
    public class LocalMinimumTest
    {
        [Test]
        public void GivenANByNArray_ShouldReturnALocalMinimum()
        {
            var input = new int[][]
            {
                new[] {1, 2},
                new[] {3, 4}
            };
            var sut = new LocalMinimum();
            var expected = 1;
            Assert.AreEqual(expected, sut.GetLocalMinimum(input));
        }
        
    }
}