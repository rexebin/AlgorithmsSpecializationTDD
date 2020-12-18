using System;

namespace Utility.BinaryTree
{
    public class BinaryTreeNode : BinaryTreeNode<int>
    {
        public BinaryTreeNode()
        {
        }

        public BinaryTreeNode(BinaryTreeNode? leftChild, BinaryTreeNode? rightChild) : base(leftChild, rightChild)
        {
        }

        public BinaryTreeNode(int item) : base(item)
        {
        }

        public BinaryTreeNode(int item, BinaryTreeNode? leftChild, BinaryTreeNode? rightChild)
            : base(item, leftChild, rightChild)
        {
        }
    }

    public class BinaryTreeNode<T> : IComparable<BinaryTreeNode<T>> where T : IComparable<T>
    {
        private BinaryTreeNode<T>? _leftChild;
        private BinaryTreeNode<T>? _rightChild;
        private int _height;
        private int _minHeight;
        public BinaryTreeNode<T> Parent { get; private set; }

        public int Height => _height - 1;

        public int MinHeight => _minHeight - 1;

        public BinaryTreeNode<T>? LeftChild
        {
            get => _leftChild;
            set
            {
                _leftChild = value;
                if (_leftChild != null) _leftChild.Parent = this;
                UpdateAllHeight();
            }
        }


        public BinaryTreeNode<T>? RightChild
        {
            get => _rightChild;
            set
            {
                _rightChild = value;
                if (_rightChild != null) _rightChild.Parent = this;
                UpdateAllHeight();
            }
        }

        private void UpdateAllHeight()
        {
            UpdateHeight();
            UpdateMinHeight();
        }

        public T? NodeValue { get; }

        public BinaryTreeNode()
        {
            _height = 1;
            _minHeight = 1;
            Parent = this;
        }

        public BinaryTreeNode(BinaryTreeNode<T>? leftChild, BinaryTreeNode<T>? rightChild) : this()
        {
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        public BinaryTreeNode(T item) : this()
        {
            NodeValue = item;
        }

        public BinaryTreeNode(T item, BinaryTreeNode<T>? leftChild, BinaryTreeNode<T>? rightChild)
            : this(leftChild, rightChild)
        {
            NodeValue = item;
        }

        private void UpdateHeight()
        {
            if (AreChildrenSameHeight()) return;
            _height = GetTallestChildHeight() + 1;
        }

        private void UpdateMinHeight()
        {
            if (_rightChild != null && _leftChild == null)
            {
                _minHeight = _rightChild._minHeight + 1;
                return;
            }

            if (_leftChild != null && _rightChild == null)
            {
                _minHeight = _leftChild._minHeight + 1;
                return;
            }

            if (AreChildrenSameMinHeight()) return;
            _minHeight = GetShortestChildMinHeight() + 1;
        }

        private int GetTallestChildHeight()
        {
            var leftChildHeight = _leftChild?._height ?? 0;
            var rightChildHeight = _rightChild?._height ?? 0;
            return leftChildHeight >= rightChildHeight ? leftChildHeight : rightChildHeight;
        }

        private bool AreChildrenSameHeight()
        {
            var leftChildHeight = _leftChild?._height ?? 0;
            var rightChildHeight = _rightChild?._height ?? 0;
            return leftChildHeight == rightChildHeight;
        }

        private bool AreChildrenSameMinHeight()
        {
            var leftChildMinHeight = _leftChild?._minHeight ?? 0;
            var rightChildMinHeight = _rightChild?._minHeight ?? 0;
            return leftChildMinHeight == rightChildMinHeight;
        }


        private int GetShortestChildMinHeight()
        {
            var leftChildMinHeight = _leftChild?._minHeight ?? 0;
            var rightChildMinHeight = _rightChild?._minHeight ?? 0;
            return leftChildMinHeight <= rightChildMinHeight ? leftChildMinHeight : rightChildMinHeight;
        }

        public int CompareTo(BinaryTreeNode<T>? other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            if (NodeValue == null && other.NodeValue == null) return 0;
            if (NodeValue == null) return -1;
            if (other.NodeValue == null) return 1;
            return NodeValue.CompareTo(other.NodeValue);
        }
    }
}