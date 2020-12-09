using System;
using System.Collections.Generic;
using System.Linq;

namespace Utility.DataStructures
{
    public interface IHeap<T> where T : Record, IComparable<T>
    {
        void Insert(T item);
        T? Pull();
        void InsertMany(IEnumerable<T> records);
        bool TryDelete(Predicate<T> predicate);
    }

    public class Heap<T> : IHeap<T> where T : Record, IComparable<T>
    {
        private readonly List<T> _backingList = new();

        public List<T> ToArray()
        {
            return _backingList;
        }

        public Heap()
        {
        }

        public Heap(IEnumerable<T> seeds)
        {
            InsertMany(seeds);
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
                if (_backingList[parent].CompareTo(_backingList[index]) != 1) return;
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

        private void BubbleDown(int i)
        {
            while (true)
            {
                var leftChild = GetLeftChild(i);
                var rightChild = GetRightChild(i);
                var smallerChild = GetSmallerChildIndex(leftChild, rightChild);
                if (smallerChild == null) return;
                if (_backingList[i].CompareTo(_backingList[smallerChild.Value]) != 1) return;
                Swap(i, smallerChild.Value);
                i = smallerChild.Value;
            }
        }

        private int? GetSmallerChildIndex(int? leftChild, int? rightChild)
        {
            return leftChild != null && rightChild != null
                ? _backingList[leftChild.Value].CompareTo(_backingList[rightChild.Value]) == -1
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