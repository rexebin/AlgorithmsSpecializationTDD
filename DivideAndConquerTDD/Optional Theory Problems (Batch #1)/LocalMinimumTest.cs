using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DivideAndConquerTDD
{
    /**
     * You are given an n by n grid of distinct numbers. A number is a local minimum if it is smaller than all of its
     * neighbors. (A neighbor of a number is one immediately above, below, to the left, or the right. Most numbers have
     * four neighbors; numbers on the side have three; the four corners have two.) Use the divide-and-conquer algorithm
     * design paradigm to compute a local minimum with only O(n) comparisons between pairs of numbers. (Note: since
     * there are n^2 numbers in the input, you cannot afford to look at all of them.
     * Hint: Think about what types of recurrences would give you the desired upper bound.)
     */
    public class LocalMinimumTest
    {
        [Test]
        public void GivenANAndNArrayAndRange_ShouldReturnLocalMinWhenCrossed()
        {
            var n = 3;
            var range = new Dimension(0, n - 1, 0, n - 1, (n - 1) / 2, -1);
            var input = new[]
            {
                new[] {4, 6, 2},
                new[] {3, 4, 5},
                new[] {5, 6, 7}
            };
            var expected = 3;
            var sut = new LocalMinimum();
            var result = sut.GetLocalMinimum(input, range);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenANAndNArrayAndRange_ShouldReturnLocalMin()
        {
            var n = 3;
            var range = new Dimension(0, n - 1, 0, n - 1, n / 2, -1);
            var input = new[]
            {
                new[] {4, 6, 2},
                new[] {8, 3, 9},
                new[] {1, -1, 5}
            };
            var expected = -1;
            var sut = new LocalMinimum();
            var result = sut.GetLocalMinimum(input, range);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenANAndNArrayAndRange_ShouldReturnLocalMin1()
        {
            var n = 4;
            var range = new Dimension(0, n - 1, 0, n - 1, n / 2 - 1, -1);
            var input = new[]
            {
                new[] {4, 6, 2, 10},
                new[] {-10, 3, 9, 15},
                new[] {-11, -80, -70, -60},
                new[] {-19, -30, -40, -50}
            };
            var expected = -80;
            var sut = new LocalMinimum();
            var result = sut.GetLocalMinimum(input, range);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GivenANAndNArrayAndRange_ShouldReturnLocalMin2()
        {
            var n = 10;
            var range = new Dimension(0, n - 1, 0, n - 1, n / 2 - 1, -1);
            var input = new[]
            {
                new[] {55, 45, 38, 23, 88, 46, 7, 89, 0, 94},
                new[] {2, 92, 43, 51, 58, 67, 82, 90, 79, 17},
                new[] {29, 64, 16, 8, 50, 14, 1, 25, 26, 73},
                new[] {97, 37, 13, 20, 4, 75, 98, 80, 48, 12},
                new[] {33, 27, 42, 74, 95, 35, 57, 53, 96, 60},
                new[] {59, 25, 76, 40, 6, 11, 77, 49, 93, 61},
                new[] {5, 72, 9, 91, 68, 30, 39, 69, 99, 21},
                new[] {52, 31, 28, 34, 3, 81, 18, 62, 10, 71},
                new[] {66, 24, 44, 54, 56, 85, 84, 22, 47, 63},
                new[] {65, 36, 83, 41, 15, 19, 87, 78, 70, 32},
            };
            var expected = 24;
            var sut = new LocalMinimum();
            var result = sut.GetLocalMinimum(input, range);
            Assert.AreEqual(expected, result);
        }
    }
}