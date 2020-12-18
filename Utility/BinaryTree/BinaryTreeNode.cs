namespace Utility.BinaryTree
{
    public class BinaryTreeNode : BinaryTreeNode<int>
    {
        public BinaryTreeNode() : base()
        {
        }

        public BinaryTreeNode(BinaryTreeNode leftChild, BinaryTreeNode rightChild) : base(leftChild, rightChild)
        {
        }

        public BinaryTreeNode(int item) : base(item)
        {
        }

        public BinaryTreeNode(int item, BinaryTreeNode leftChild, BinaryTreeNode rightChild)
            : base(item, leftChild, rightChild)
        {
        }
    }

    public class BinaryTreeNode<T>
    {
        private BinaryTreeNode<T>? _leftChild;
        private BinaryTreeNode<T>? _rightChild;
        public BinaryTreeNode<T> Parent { get; private set; }
        public int Height { get; private set; }

        public BinaryTreeNode<T>? LeftChild
        {
            get => _leftChild;
            set
            {
                _leftChild = value;
                if (_leftChild != null) _leftChild.Parent = this;
                UpdateHeight();
            }
        }

        public BinaryTreeNode<T>? RightChild
        {
            get => _rightChild;
            set
            {
                _rightChild = value;
                if (_rightChild != null) _rightChild.Parent = this;
                UpdateHeight();
            }
        }

        public int MinHeight => AreChildrenSameHeight() ? Height : GetShortestChildHeight() + 1;
        public T? NodeValue { get; }

        public BinaryTreeNode()
        {
            Height = 1;
            Parent = this;
        }

        public BinaryTreeNode(BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild) : this()
        {
            LeftChild = leftChild;
            RightChild = rightChild;
        }

        public BinaryTreeNode(T item) : this()
        {
            NodeValue = item;
        }

        public BinaryTreeNode(T item, BinaryTreeNode<T> leftChild, BinaryTreeNode<T> rightChild)
            : this(leftChild, rightChild)
        {
            NodeValue = item;
        }

        private void UpdateHeight()
        {
            if (AreChildrenSameHeight()) return;
            Height = GetTallestChildHeight() + 1;
        }

        private int GetTallestChildHeight()
        {
            var leftChildHeight = _leftChild?.Height ?? 0;
            var rightChildHeight = _rightChild?.Height ?? 0;
            return leftChildHeight >= rightChildHeight ? leftChildHeight : rightChildHeight;
        }

        private bool AreChildrenSameHeight()
        {
            var leftChildHeight = _leftChild?.Height ?? 0;
            var rightChildHeight = _rightChild?.Height ?? 0;
            return leftChildHeight == rightChildHeight;
        }


        private int GetShortestChildHeight()
        {
            var leftChildHeight = LeftChild?.Height ?? 0;
            var rightChildHeight = _rightChild?.Height ?? 0;
            return leftChildHeight <= rightChildHeight ? leftChildHeight : rightChildHeight;
        }
    }
}