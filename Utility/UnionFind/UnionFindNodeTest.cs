using NUnit.Framework;
using Utility.DataStructures;

namespace Utility.UnionFind
{
    public class UnionFindNodeTest
    {
        [Test]
        public void GivenT_ShouldCreateNewUnionFindNodeWithInitValues()
        {
            var item = 1;
            var node = new UnionFindNode<int>(item);
            Assert.AreEqual(item, node.Value);
            Assert.AreEqual(node, node.Parent);
            Assert.AreEqual(0, node.Rank);
            Assert.AreEqual(1, node.Children.Count);
        }

        [Test]
        public void GivenTwoNodesWithSameRank_DoUnion_ShouldUnionGivenNodeAsChild_AndIncrementItsOwnRankByOne()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(1, node1.Rank);
            Assert.AreEqual(0, node2.Rank);
            Assert.AreEqual(2, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
        }


        [Test]
        public void GivenHigherRootRankNode_DoUnion_ShouldSetItsOwnParentToGivenNode_NoRankChanges()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            var node3 = new UnionFindNode<int>(3);
            node1.Union(node2);
            node3.Union(node1);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(1, node1.Rank);
            Assert.AreEqual(0, node2.Rank);
            Assert.AreEqual(0, node3.Rank);
            Assert.AreEqual(3, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(1, node3.Children.Count);
        }

        [Test]
        public void GivenNodeWithHigherRootRankNode_DoUnion_ShouldSetItsOwnParentToGivenNode_NoRankChanges()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            var node3 = new UnionFindNode<int>(2);
            node1.Union(node2);
            node3.Union(node2);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(1, node1.Rank);
            Assert.AreEqual(0, node2.Rank);
            Assert.AreEqual(0, node3.Rank);
            Assert.AreEqual(3, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(1, node3.Children.Count);
        }

        [Test]
        public void GivenLowerRootRankNode_DoUnion_ShouldSetRootOfGivenNodeGivenNode_NoRankChanges()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            var node3 = new UnionFindNode<int>(3);
            node1.Union(node2);
            node1.Union(node3);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(1, node1.Rank);
            Assert.AreEqual(0, node2.Rank);
            Assert.AreEqual(0, node3.Rank);
            Assert.AreEqual(3, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(1, node3.Children.Count);
        }

        [Test]
        public void
            GivenTwoNodesWithRootsWithSameRank_DoUnion_ShouldUnionRootOfGivenNodeAsChild_AndIncrementItsRootOwnRankByOne()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node3, node4.Parent);
            Assert.AreEqual(2, node1.Rank);
            Assert.AreEqual(0, node2.Rank);
            Assert.AreEqual(1, node3.Rank);
            Assert.AreEqual(0, node4.Rank);
            Assert.AreEqual(4, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }

        [Test]
        public void Find_ShouldReturnRootNode()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            Assert.AreEqual(node1, node4.Find());
            Assert.AreEqual(4, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }

        [Test]
        public void Find_ShouldReturnRootNode_AlsoCompressPath()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            Assert.AreEqual(node1, node4.Find());
            Assert.AreEqual(node1, node4.Parent);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(4, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }

        [Test]
        public void Union_AlsoCompressPath()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            var node5 = new UnionFindNode<int>(1);
            node5.Union(node4);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node4.Parent);
            Assert.AreEqual(node1, node5.Parent);
            Assert.AreEqual(5, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
            Assert.AreEqual(1, node5.Children.Count);
        }

        [Test]
        public void UnionTwoGroupOfNode_ShouldKeepCorrectChildrenCountOnRoot()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            var node5 = new UnionFindNode<int>(1);
            node5.Union(node4);

            Assert.AreEqual(5, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
            Assert.AreEqual(1, node5.Children.Count);
            var node6 = new UnionFindNode<int>(1);
            var node7 = new UnionFindNode<int>(1);
            var node8 = new UnionFindNode<int>(1);
            node6.Union(node7);
            node6.Union(node8);
            node5.Union(node8);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node4.Parent);
            Assert.AreEqual(node1, node5.Parent);
            Assert.AreEqual(node1, node6.Parent);
            Assert.AreEqual(node6, node7.Parent);
            Assert.AreEqual(node6, node8.Parent);

            Assert.AreEqual(8, node1.Children.Count);
        }


        [Test]
        public void UnionAlreadyUnionedNodes_ShouldNotUnionAgain()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            node2.Union(node4);
            Assert.AreEqual(node1, node1.Parent);
            Assert.AreEqual(2, node1.Rank);
            Assert.AreEqual(node1, node2.Parent);
            Assert.AreEqual(node1, node3.Parent);
            Assert.AreEqual(node1, node4.Parent);
            Assert.AreEqual(4, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }

        [Test]
        public void IsSameUnion_ShouldReturnTrueGivenNodeIsInSameUnion()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            node1.Union(node3);
            Assert.True(node3.IsSameUnion(node4));
            Assert.AreEqual(4, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }

        [Test]
        public void IsSameUnion_ShouldReturnFalseGivenNodeIsNotInSameUnion()
        {
            var node1 = new UnionFindNode<int>(1);
            var node2 = new UnionFindNode<int>(2);
            node1.Union(node2);
            var node3 = new UnionFindNode<int>(1);
            var node4 = new UnionFindNode<int>(2);
            node3.Union(node4);
            Assert.False(node2.IsSameUnion(node3));
            Assert.AreEqual(2, node1.Children.Count);
            Assert.AreEqual(1, node2.Children.Count);
            Assert.AreEqual(2, node3.Children.Count);
            Assert.AreEqual(1, node4.Children.Count);
        }
        
        [Test]
        public void GivenItem_DoToUnionFindNode_ShouldReturnUnionFindNodeWithValueToItemRankToZeroParentToItsSelf()
        {
            var item = new TestRecord(1, 2);
            var node = UnionFindNode<TestRecord>.Parse(item);
            Assert.AreEqual(node, node.Parent);
            Assert.AreEqual(0, node.Rank);
            Assert.AreEqual(item, node.Value);
        }
    }
}