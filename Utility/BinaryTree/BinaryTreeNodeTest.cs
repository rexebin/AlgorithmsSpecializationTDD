using System;
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
            var a = new BinaryTreeNode();
            var b = new BinaryTreeNode();
            var c = new BinaryTreeNode();
            var d = new BinaryTreeNode();
            var e = new BinaryTreeNode();
            Assert.AreEqual(0, a.Height);
            var cd = new BinaryTreeNode(c, d);
            Assert.AreEqual(1, cd.Height);
            var be = new BinaryTreeNode(b, e);
            var becd = new BinaryTreeNode(be, cd);
            Assert.AreEqual(2, becd.Height);
            var abcde = new BinaryTreeNode(a, becd);
            Assert.AreEqual(3, abcde.Height);
        }

        [Test]
        public void GivenNullToChildren_ShouldNotAffectHeight()
        {
            var a = new BinaryTreeNode();
            a.LeftChild = null;
            Assert.AreEqual(0, a.Height);
            Assert.AreEqual(0, a.MinHeight);
            a.RightChild = null;
            Assert.AreEqual(0, a.Height);
            Assert.AreEqual(0, a.MinHeight);
        }

        [Test]
        public void ShouldCalculateMinimumHeight()
        {
            var a = new BinaryTreeNode();
            var b = new BinaryTreeNode();
            var c = new BinaryTreeNode();
            var d = new BinaryTreeNode();
            var e = new BinaryTreeNode();
            var cd = new BinaryTreeNode(c, d);
            var be = new BinaryTreeNode(b, e);
            var becd = new BinaryTreeNode(be, cd);
            var abcde = new BinaryTreeNode(a, becd);
            Assert.AreEqual(1, abcde.MinHeight);
        }

        [Test]
        public void ShouldGetHeightAndMinHeightOnComplexTree()
        {
            var a = new BinaryTreeNode();
            var b = new BinaryTreeNode();
            var c = new BinaryTreeNode();
            var d = new BinaryTreeNode();
            var e = new BinaryTreeNode();
            var f = new BinaryTreeNode();
            var be = new BinaryTreeNode(b, e);
            var abe = new BinaryTreeNode(a, be);
            var abed = new BinaryTreeNode(abe, d);
            var cf = new BinaryTreeNode(c, f);
            var abcdef = new BinaryTreeNode();
            abcdef.LeftChild = abed;
            abcdef.RightChild = cf;
            Assert.AreEqual(4, abcdef.Height);
            Assert.AreEqual(2, abcdef.MinHeight);
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
            Assert.AreEqual(4, abcdef.Height);
            Assert.AreEqual(2, abcdef.MinHeight);
        }

        [Test]
        public void ShouldCompare()
        {
            var a = new BinaryTreeNode(3);
            var b = new BinaryTreeNode(2);
            var c = new BinaryTreeNode(6);
            var d = new BinaryTreeNode(6);

            Assert.True(a.CompareTo(b) == 1);
            Assert.True(b.CompareTo(c) == -1);
            Assert.True(c.CompareTo(d) == 0);

            var e = new BinaryTreeNode<string>("abc");
            var f = new BinaryTreeNode<string>();
            var g = new BinaryTreeNode<string>();
            Assert.AreEqual(-1, f.CompareTo(e));
            Assert.AreEqual(1, e.CompareTo(f));
            Assert.AreEqual(0, g.CompareTo(f));
        }
    }
}