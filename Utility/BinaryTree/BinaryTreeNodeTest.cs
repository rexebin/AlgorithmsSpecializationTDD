using NUnit.Framework;

namespace Utility.BinaryTree
{
    public class BinaryTreeNodeTest
    {
        [Test]
        public void ShouldCreateNodeWithNoChildrenParent()
        {
            var a = new BinaryTreeNode<string>("A");
            Assert.AreEqual("A", a.NodeValue);
            Assert.Null(a.LeftChild);
            Assert.Null(a.RightChild);
            Assert.AreEqual(a, a.Parent);
        }

        [Test]
        public void ShouldMaintainParentPointer()
        {
            var a = new BinaryTreeNode();
            var b = new BinaryTreeNode();
            var c = new BinaryTreeNode();
            var d = new BinaryTreeNode();
            var e = new BinaryTreeNode();
            Assert.AreEqual(a, a.Parent);
            var cd = new BinaryTreeNode(c, d);
            Assert.AreEqual(cd, c.Parent);
            Assert.AreEqual(cd, d.Parent);
            Assert.AreEqual(cd, cd.Parent);
            var be = new BinaryTreeNode(b, e);
            var becd = new BinaryTreeNode(be, cd);
            Assert.AreEqual(becd, be.Parent);
            Assert.AreEqual(becd, cd.Parent);
            Assert.AreEqual(becd, becd.Parent);
            var abcde = new BinaryTreeNode(a, becd);
            Assert.AreEqual(abcde, a.Parent);
            Assert.AreEqual(abcde, becd.Parent);
            Assert.AreEqual(abcde, abcde.Parent);
        }

        [Test]
        public void ShouldMaintainHeight_ParentHeightEqualsLargerChildHeightPlusOne()
        {
            var a = new BinaryTreeNode<string>();
            var b = new BinaryTreeNode<string>();
            var c = new BinaryTreeNode<string>();
            var d = new BinaryTreeNode<string>();
            var e = new BinaryTreeNode<string>();
            Assert.AreEqual(1, a.Height);
            var cd = new BinaryTreeNode<string>(c, d);
            Assert.AreEqual(2, cd.Height);
            var be = new BinaryTreeNode<string>(b, e);
            var becd = new BinaryTreeNode<string>(be, cd);
            Assert.AreEqual(3, becd.Height);
            var abcde = new BinaryTreeNode<string>(a, becd);
            Assert.AreEqual(4, abcde.Height);
        }

        [Test]
        public void ShouldCalculateMinimumHeight()
        {
            var a = new BinaryTreeNode<string>();
            var b = new BinaryTreeNode<string>();
            var c = new BinaryTreeNode<string>();
            var d = new BinaryTreeNode<string>();
            var e = new BinaryTreeNode<string>();
            var cd = new BinaryTreeNode<string>(c, d);
            var be = new BinaryTreeNode<string>(b, e);
            var becd = new BinaryTreeNode<string>(be, cd);
            var abcde = new BinaryTreeNode<string>(a, becd);
            Assert.AreEqual(2, abcde.MinHeight);
        }

        [Test]
        public void ShouldGetHeightAndMinHeightOnComplexTree()
        {
            var a = new BinaryTreeNode<string>();
            var b = new BinaryTreeNode<string>();
            var c = new BinaryTreeNode<string>();
            var d = new BinaryTreeNode<string>();
            var e = new BinaryTreeNode<string>();
            var f = new BinaryTreeNode<string>();
            var be = new BinaryTreeNode<string>(b, e);
            var abe = new BinaryTreeNode<string>(a, be);
            var abed = new BinaryTreeNode<string>(abe, d);
            var cf = new BinaryTreeNode<string>(c, f);
            var abcdef = new BinaryTreeNode<string>();
            abcdef.LeftChild = abed;
            abcdef.RightChild = cf;
            Assert.AreEqual(5, abcdef.Height);
            Assert.AreEqual(3, abcdef.MinHeight);
        }

        [Test]
        public void ShouldGetHeightAndMinHeightOnComplexGenericTree()
        {
            var a = new BinaryTreeNode(3);
            var b = new BinaryTreeNode(2);
            var c = new BinaryTreeNode(6);
            var d = new BinaryTreeNode(8);
            var e = new BinaryTreeNode(2);
            var f = new BinaryTreeNode(6);
            var be = new BinaryTreeNode(2 + 2, b, e);
            var abe = new BinaryTreeNode(3 + 2 + 2, a, be);
            var abed = new BinaryTreeNode(3 + 2 + 2 + 8, abe, d);
            var cf = new BinaryTreeNode(6 + 6, c, f);
            var abcdef = new BinaryTreeNode(3 + 2 + 2 + 8 + 6 + 6);
            abcdef.LeftChild = abed;
            abcdef.RightChild = cf;
            Assert.AreEqual(5, abcdef.Height);
            Assert.AreEqual(3, abcdef.MinHeight);
        }
    }
}