using System;
using NUnit.Framework;

namespace DivideAndConquerTDD.AssignmentThree
{
    public class QuickSortTest
    {
        [Test]
        [TestCase(new[] {3, 8, 2, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 15)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 15)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6, 15, 14, 13, 11, 10, 9, 12},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, 51)]
        public void GivenArray_ShouldUseFirstElementAsPivot_ReturnPartitionedArrays(int[] input, int[] expected,
            int count)
        {
            var sut = new QuickSort();
            var result = sut.Sort(input, 0, input.Length);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(count, sut.GetCount());
        }

        [Test]
        public void ShouldReadArrayFromText_UseFirstElementAsPivot_SortAndReturnCompareCount()
        {
            var sut = new QuickSort();
            var count = sut.Sort();
            Assert.AreEqual(162085, count);
        }

        [Test]
        [TestCase(new[] {3, 8, 2, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 15)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 15)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6, 15, 14, 13, 11, 10, 9, 12},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, 43)]
        public void GivenArray_ShouldUseLastElementAsPivot_ReturnPartitionedArrays(int[] input, int[] expected,
            int count)
        {
            var sut = new QuickSort();
            var result = sut.Sort(input, 0, input.Length, PivotMode.LastElement);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(count, sut.GetCount());
        }

        [Test]
        public void ShouldSwapFirstAndLastElement()
        {
            var sut = new QuickSort();
            var input = sut.SwapLastAndFirstElement(new[] {1, 2, 3, 4, 5}, 0, 5);
            Assert.AreEqual(new int[] {5, 2, 3, 4, 1}, input);
        }


        [Test]
        public void ShouldReadArrayFromText_UseLastElementAsPivot_SortAndReturnCompareCount()
        {
            var sut = new QuickSort();
            var count = sut.Sort(PivotMode.LastElement);
            Assert.AreEqual(164123, count);
        }

        [Test]
        [TestCase(new[] {4, 5, 6, 7}, 1)]
        [TestCase(new[] {4, 5, 6, 7, 8}, 2)]
        public void GivenArray4567_ShouldReturnMiddleIndex(int[] input, int expected)
        {
            var sut = new QuickSort();
            var median = sut.GetMiddleIndex(input);
            Assert.AreEqual(expected, median);
        }

        [Test]
        [TestCase(new[] {4, 5, 6, 7}, 0, 4, 1)]
        [TestCase(new[] {4, 5, 3, 7, 8}, 0, 5, 0)]
        [TestCase(new[] {4, 5, 2, 7, 3}, 0, 5, 4)]
        [TestCase(new[] {9, 2, 4, 6, 7, 8, 9, 3, 4}, 0, 9, 4)]
        [TestCase(new[] {4, 2, 4, 6, 7, 8, 9, 3, 9}, 0, 9, 4)]
        [TestCase(new[] {9, 2, 4, 6, 4, 8, 9, 3, 7}, 0, 9, 8)]
        [TestCase(new[] {4, 2, 4, 6, 9, 8, 9, 3, 7}, 0, 9, 8)]
        [TestCase(new[] {7, 2, 4, 6, 4, 8, 9, 3, 9}, 0, 9, 0)]
        [TestCase(new[] {7, 2, 4, 6, 9, 8, 9, 3, 4}, 0, 9, 0)]
        [TestCase(new[] {9, 2, 4, 6, 4, 8, 3, 7}, 0, 8, 7)]
        [TestCase(new[] {9, 2, 4, 6, 9, 8, 3, 7}, 0, 8, 7)]
        [TestCase(new[] {9, 2, 4, 7, 4, 8, 3, 6}, 0, 8, 3)]
        [TestCase(new[] {6, 2, 4, 7, 4, 8, 3, 9}, 0, 8, 3)]
        [TestCase(new[] {7, 2, 4, 9, 4, 8, 3, 6}, 0, 8, 0)]
        [TestCase(new[] {7, 2, 4, 6, 4, 8, 3, 9}, 0, 8, 0)]
        public void GivenArray4567_ShouldCompareAndReturnMedianIndex(int[] input, int left, int right, int expected)
        {
            var sut = new QuickSort();
            var median = sut.GetMedianIndex(input, left, right);
            Assert.AreEqual(expected, median);
        }


        [Test]
        [TestCase(new[] {1, 2, 3, 4, 5}, 0, 5, new int[] {3, 2, 1, 4, 5})]
        [TestCase(new[] {1, 2, 3, 4}, 0, 4, new int[] {2, 1, 3, 4})]
        [TestCase(new[] {9, 2, 4, 6, 7, 8, 9, 3, 4}, 0, 9, new int[] {7, 2, 4, 6, 9, 8, 9, 3, 4})]
        [TestCase(new[] {4, 2, 4, 6, 7, 8, 9, 3, 9}, 0, 9, new []{7, 2, 4, 6, 4, 8, 9, 3, 9})]
        [TestCase(new[] {9, 2, 4, 6, 4, 8, 9, 3, 7}, 0, 9, new []{7, 2, 4, 6, 4, 8, 9, 3, 9})]
        [TestCase(new[] {4, 2, 4, 6, 9, 8, 9, 3, 7}, 0, 9, new []{7, 2, 4, 6, 9, 8, 9, 3, 4})]
        [TestCase(new[] {7, 2, 4, 6, 4, 8, 9, 3, 9}, 0, 9, new []{7, 2, 4, 6, 4, 8, 9, 3, 9})]
        [TestCase(new[] {7, 2, 4, 6, 9, 8, 9, 3, 4}, 0, 9, new []{7, 2, 4, 6, 9, 8, 9, 3, 4})]
        [TestCase(new[] {9, 2, 4, 6, 4, 8, 3, 7}, 0, 8, new []{7, 2, 4, 6, 4, 8, 3, 9})]
        [TestCase(new[] {9, 2, 4, 6, 4, 8, 3, 7}, 0, 8, new []{7, 2, 4, 6, 4, 8, 3, 9})]
        [TestCase(new[] {9, 2, 4, 7, 4, 8, 3, 6}, 0, 8, new []{7, 2, 4, 9, 4, 8, 3, 6})]
        [TestCase(new[] {6, 2, 4, 7, 4, 8, 3, 9}, 0, 8, new []{7, 2, 4, 6, 4, 8, 3, 9})]
        [TestCase(new[] {7, 2, 4, 9, 4, 8, 3, 6}, 0, 8, new []{7, 2, 4, 9, 4, 8, 3, 6})]
        [TestCase(new[] {7, 2, 4, 6, 4, 8, 3, 9}, 0, 8, new []{7, 2, 4, 6, 4, 8, 3, 9})]
        public void ShouldSwapMedianAndFirstElement(int[] input, int left, int right, int[] expected)
        {
            var sut = new QuickSort();
            var result = sut.SwapMedianWithFirstElement(input, left, right);
            Assert.AreEqual(expected, result);
        }


        [Test]
        [TestCase(new[] {3, 8, 2, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 16)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6}, new[] {1, 2, 3, 4, 5, 6, 7, 8}, 16)]
        [TestCase(new[] {3, 2, 8, 5, 1, 4, 7, 6, 15, 14, 13, 11, 10, 9, 12},
            new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15}, 41)]
        public void GivenArray_ShouldMedianAsPivot_ReturnPartitionedArrays(int[] input, int[] expected,
            int count)
        {
            var sut = new QuickSort();
            var result = sut.Sort(input, 0, input.Length, PivotMode.Median);
            Assert.AreEqual(expected, result);
            Assert.AreEqual(count, sut.GetCount());
        }

        [Test]
        public void ShouldReadArrayFromText_UseMedianElementAsPivot_SortAndReturnCompareCount()
        {
            var sut = new QuickSort();
            var count = sut.Sort(PivotMode.Median);
            Assert.AreEqual(156383, count);
        }
        
        [Test]
        public void ShouldReadArrayFromText_UseRandomElementAsPivot_SortAndReturnCompareCount()
        {
            var sut = new QuickSort();
            var count = sut.Sort(PivotMode.Random);
            Console.WriteLine(count);
        }
    }
}