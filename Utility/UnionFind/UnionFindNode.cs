using System.Collections.Generic;

namespace Utility.UnionFind
{
    public class UnionFindNode<T> : IUnionFindNode<T>
    {
        public T Value { get; }
        public int Rank { get; private set; }
        public UnionFindNode<T> Parent { get; private set; }

        public UnionFindNode(T value)
        {
            Value = value;
            Rank = 0;
            Parent = this;
        }

        public void Union(UnionFindNode<T> node)
        {
            var rootOfGivenNode = GetRootNode(node);
            var root = GetRootNode(this);
            if (root.Rank == rootOfGivenNode.Rank)
            {
                rootOfGivenNode.Parent = root;
                root.IncrementRank();
                return;
            }

            if (root.Rank > rootOfGivenNode.Rank)
            {
                rootOfGivenNode.Parent = root;
                return;
            }

            root.Parent = rootOfGivenNode;
        }

        private void IncrementRank()
        {
            Rank++;
        }

        private UnionFindNode<T> GetRootNode(UnionFindNode<T> node)
        {
            var compressNodes = new List<UnionFindNode<T>>();
            UnionFindNode<T> root;
            while (true)
            {
                var parent = node.Parent;
                if (parent.Parent == parent)
                {
                    root = parent;
                    break;
                }

                compressNodes.Add(node);
                node = parent;
            }

            CompressPath(compressNodes, root);
            return root;
        }

        private static void CompressPath(List<UnionFindNode<T>> nodes, UnionFindNode<T> root)
        {
            foreach (var n in nodes) n.Parent = root;
        }

        public UnionFindNode<T> Find()
        {
            return GetRootNode(this);
        }

        public bool IsSameUnion(UnionFindNode<T> node)
        {
            return GetRootNode(node) == GetRootNode(this);
        }

        public static UnionFindNode<T> Parse(T item)
        {
            return new(item);
        }
    }
}