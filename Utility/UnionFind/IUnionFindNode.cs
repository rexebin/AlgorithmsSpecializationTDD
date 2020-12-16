namespace Utility.UnionFind
{
    public interface IUnionFindNode<T>
    {
        bool IsSameUnion(UnionFindNode<T> node);
        UnionFindNode<T> Find();
        void Union(UnionFindNode<T> node);
    }
}