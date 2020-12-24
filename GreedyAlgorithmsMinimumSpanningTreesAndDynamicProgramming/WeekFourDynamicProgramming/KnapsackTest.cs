using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekFourDynamicProgramming
{
    public class KnapsackTest
    {
        [Test]
        public void GivenItems_ShouldGenerateTwoDimensionalList()
        {
            var items = new List<Item>
            {
                new(3, 4),
                new(2, 3),
                new(4, 2),
                new(4, 3)
            };
            var sut = new Knapsack();
            var result = new List<List<int>>
            {
                new() {0, 0, 0, 0, 0, 0, 0},
                new() {0, 0, 0, 0, 3, 3, 3},
                new() {0, 0, 0, 2, 3, 3, 3},
                new() {0, 0, 4, 4, 4, 6, 7},
                new() {0, 0, 4, 4, 4, 8, 8}
            };
            Assert.AreEqual(result, sut.ProcessItems(items, 6));
        }

        [Test]
        public void ShouldReadFileToItems()
        {
            var sut = new Knapsack();
            var items = sut.ReadFile("knapsack1.txt");
            Assert.AreEqual(100, items.Count);
            Assert.AreEqual(new Item(16808, 250), items.First());
        }

        [Test]
        public void ShouldGetOptimalResult()
        {
            var sut = new Knapsack();
            var result = sut.GetOptimalResult("knapsack1.txt", 10000);
            Assert.AreEqual(2493893, result);
        }
    }

    public record Item(int Value, int Weight);
}