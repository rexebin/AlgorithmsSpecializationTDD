using System;
using System.Collections.Generic;
using System.Linq;
using Utility.BinaryTree;
using Utility.Common;
using Utility.Heaps;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekThreeHuffmanDynamicProgramming
{
    public class Huffman
    {
        public List<int> GetSymbols(string filename)
        {
            return new FileReader().ReadFile(filename).Skip(1).Select(int.Parse).ToList();
        }

        public MinHeap<BinaryTreeNode> TransformToMinHeap(List<int> symbols)
        {
            return new(symbols.Select(s => new BinaryTreeNode(s)));
        }

        public int GetMaxLength()
        {
            var minHeap = RunHuffman();
            return minHeap.Peek()?.Height ?? 0;
        }

        public int GetMinLength()
        {
            var minHeap = RunHuffman();
            return minHeap.Peek()?.MinHeight ?? 0;
        }

        private MinHeap<BinaryTreeNode> RunHuffman()
        {
            var minHeap = TransformToMinHeap(GetSymbols("Huffman.txt"));
            while (minHeap.Count != 1)
            {
                var min1 = minHeap.Pull();
                var min2 = minHeap.Pull();
                var newNode = new BinaryTreeNode((min1?.NodeValue ?? 0) + (min2?.NodeValue ?? 0), min2, min1);
                minHeap.Insert(newNode);
            }

            return minHeap;
        }
    }
}