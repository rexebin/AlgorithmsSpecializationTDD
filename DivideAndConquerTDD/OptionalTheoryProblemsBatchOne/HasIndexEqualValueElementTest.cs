using NUnit.Framework;

namespace DivideAndConquerTDD.OptionalTheoryProblemsBatchOne
{
    /**
     * You are given a sorted (from smallest to largest) array A of n distinct integers which can be positive,
     * negative, or zero. You want to decide whether or not there is an index i such that A[i] = i.
     * Design the fastest algorithm that you can for solving this problem.
     */
    public class HasIndexEqualValueElementTest
    {
        [Test]
        [TestCase(new[] {1, 2}, false)]
        [TestCase(new[] {1, 2,3,4,5,6}, false)]
        [TestCase(new[] {-1, 0,1,2,4,6}, true)]
        [TestCase(new[] {-100, -10,0,1,3,4,5,6,7,8,9}, false)]
        [TestCase(new[] {-100, -10,0,1,3,4,5,6,7,8,9,11}, true)]
        [TestCase(new[] {-100, -10,-9,-8,-6,-5,1,3,4,5,6,7,8,9,11,15}, true)]
        [TestCase(new[] {0, 10,11,12,13,14,15,16}, true)]
        [TestCase(new[] {-300, -200,-100,0,1,2,3,4,5,6,7,8,9,16,17,18,19,20,21,22,23,24,25,26,27,40,41,42,45,46,48,90,91,92,100}, false)]
        [TestCase(new[] {-300, -200,-100,0,1,2,3,4,5,6,7,8,9,13,17,18,19,20,21,22,23,24,25,26,27,40,41,42,45,46,48,90,91,92,100}, true)]
        public void GivenSortedArray_ShouldReturnTrueIfFondTargetOrFalse(int[] numbers, bool expected)
        {
            var sut = new HasIndexEqualValueElement();
            Assert.AreEqual(expected, sut.ContainsValueEqualIndex(numbers));
            sut.PrintCount();
        }
    }
}