using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.DataStructures
{
    public abstract record Record;

    public interface IHeap<T> where T : Record, IComparable<T>
    {
        void Insert(T item);
        T? Pull();
        void InsertMany(IEnumerable<T> records);
        bool TryDelete(Predicate<T> predicate);
    }

    public class MinHeap<T> : Heap<T> where T : Record, IComparable<T>
    {
        public MinHeap() : base(true)
        {
        }

        public MinHeap(IEnumerable<T> seeds) : base(seeds, true)
        {
        }
    }

    public class MaxHeap<T> : Heap<T> where T : Record, IComparable<T>
    {
        public MaxHeap() : base(false)
        {
        }

        public MaxHeap(IEnumerable<T> seeds) : base(seeds, false)
        {
        }
    }

    public class Heap<T> : IHeap<T> where T : Record, IComparable<T>
    {
        private readonly bool _isMin;
        private readonly List<T> _backingList = new();
        private int CompareResult => _isMin ? 1 : -1;

        public Heap(bool isMin)
        {
            _isMin = isMin;
        }

        public Heap(IEnumerable<T> seeds, bool isMin) : this(isMin)
        {
            InsertMany(seeds);
        }

        public List<T> ToArray()
        {
            return _backingList;
        }

        public void Insert(T item)
        {
            _backingList.Add(item with {});
            BubbleUp(_backingList.Count - 1);
        }

        private void BubbleUp(int index)
        {
            while (true)
            {
                var parent = GetParentIndex(index);
                if (_backingList[parent].CompareTo(_backingList[index]) != CompareResult) return;
                Swap(index, parent);
                index = parent;
            }
        }


        private int GetParentIndex(int index)
        {
            return (index - 1) / 2;
        }

        public T? Pull()
        {
            if (!_backingList.Any()) return null;
            var top = _backingList[0];
            Swap(0, _backingList.Count - 1);
            _backingList.RemoveAt(_backingList.Count - 1);
            BubbleDown(0);
            return top;
        }

        private void BubbleDown(int index)
        {
            while (true)
            {
                var smallerChild = GetSmallerChildIndex(index);
                if (smallerChild == null) return;
                if (_backingList[index].CompareTo(_backingList[smallerChild.Value]) != CompareResult) return;
                Swap(index, smallerChild.Value);
                index = smallerChild.Value;
            }
        }

        private int? GetSmallerChildIndex(int parent)
        {
            var leftChild = GetLeftChild(parent);
            var rightChild = GetRightChild(parent);
            return leftChild != null && rightChild != null
                ? _backingList[rightChild.Value].CompareTo(_backingList[leftChild.Value]) == CompareResult
                    ? leftChild
                    : rightChild
                : leftChild;
        }

        private void Swap(int index1, int index2)
        {
            var buff = _backingList[index1];
            _backingList[index1] = _backingList[index2];
            _backingList[index2] = buff;
        }

        private int? GetRightChild(int i)
        {
            var index = i * 2 + 2;
            if (index < _backingList.Count) return index;
            return null;
        }

        private int? GetLeftChild(int i)
        {
            var index = i * 2 + 1;
            if (index < _backingList.Count) return index;
            return null;
        }

        public void InsertMany(IEnumerable<T> records)
        {
            foreach (var testRecord in records) Insert(testRecord);
        }

        public bool TryDelete(Predicate<T> predicate)
        {
            var itemToDelete = _backingList.Find(predicate);
            if (itemToDelete == null)
                return false;

            var index = _backingList.IndexOf(itemToDelete);
            Swap(index, _backingList.Count - 1);
            _backingList.RemoveAt(_backingList.Count - 1);
            BubbleDown(index);
            return true;
        }
    }
}