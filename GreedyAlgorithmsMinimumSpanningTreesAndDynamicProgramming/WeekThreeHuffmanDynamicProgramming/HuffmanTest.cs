using System.Collections.Generic;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class HuffmanTest
    {
        private Huffman _sut = null!;
        private List<int> _symbols = new();

        [SetUp]
        public void ShouldReadFileToList()
        {
            _sut = new Huffman();
            _symbols = _sut.GetSymbols("Huffman.txt");
            Assert.AreEqual(1000, _symbols.Count);
        }

        [Test]
        public void GivenListOfSymbols_ShouldTransformToMinHeapOfBinaryTreeNode()
        {
            var minHeap = _sut.TransformToMinHeap(_symbols);
            Assert.AreEqual(1000, minHeap.Count);
            Assert.AreEqual(1873, minHeap.Peek()?.NodeValue);
        }

        [Test]
        public void ShouldGetMaxLengthOfACodeWordInHuffmanCode()
        {
            var maxLength = _sut.GetMaxLength();
            Assert.AreEqual(19, maxLength);
        }

        [Test]
        public void ShouldGetMinLengthOfACodeWordInHuffmanCode()
        {
            var minHeight = _sut.GetMinLength();
            Assert.AreEqual(9, minHeight);
        }
    }
}