using NUnit.Framework;

namespace DivideAndConquerTDD.AssignmentTwo
{
    public class CountingInversionsTest
    {
        [Test]
        [TestCase(new[] {1, 3, 5}, new[] {2, 4, 6}, new[] {1, 2, 3, 4, 5, 6}, 3)]
        public void GivenTwoSortedArrays_ShouldMergeSortAndCountInversions(int[] arrayB, int[] arrayC,
            int[] mergedArray, int inversions)
        {
            var sut = new CountingInversions();
            var result = sut.MergeAndSort(arrayB, arrayC);
            var inversionResult = sut.GetInversions();
            Assert.AreEqual(mergedArray, result);
            Assert.AreEqual(inversions, inversionResult);
        }


        [Test]
        [TestCase(new[] {1}, new[] {1}, 0)]
        [TestCase(new[] {2, 1}, new[] {1, 2}, 1)]
        [TestCase(new[] {1, 2}, new[] {1, 2}, 0)]
        [TestCase(new[] {1, 3, 5, 2, 4, 6}, new[] {1, 2, 3, 4, 5, 6}, 3)]
        [TestCase(new[] {6, 5, 4, 3, 2, 1}, new[] {1, 2, 3, 4, 5, 6}, 15)]
        [TestCase(new[] {6, 5, 4, 3, 2, 1, 0}, new[] {0, 1, 2, 3, 4, 5, 6}, 21)]
        public void GivenArray_ShouldReturnSortedArrayAndCountInversions(int[] input, int[] sortedArray, int inversions)
        {
            var sut = new CountingInversions();
            var result = sut.SortAndCountInversions(input);
            var inversionsResult = sut.GetInversions();
            Assert.AreEqual(sortedArray, result);
            Assert.AreEqual(inversions, inversionsResult);
        }

        [Test]
        public void ShouldGetIntegerArrayPath()
        {
            var sut = new CountingInversions();
            var path = sut.GetPath();
            Assert.True(path.Contains("DivideAndConquerTDD\\DivideAndConquerTDD\\AssignmentTwo\\IntegerArray.txt"));
        }

        [Test]
        public void GivenTextFile_ShouldReturnSortedArrayAndCountInversions()
        {
            var sut = new CountingInversions();
            var result = sut.SortAndCountInversions();
            var inversionsResult = sut.GetInversions();
            Assert.AreEqual(2407905288, inversionsResult);
        }
    }
}