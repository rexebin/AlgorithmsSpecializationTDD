using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Utility.DataStructures
{
    public record TestRecord(int Id, int Length) : Record, IComparable<TestRecord>
    {
        public int CompareTo(TestRecord? record)
        {
            if (ReferenceEquals(this, record)) return 0;
            if (record == null) return 1;
            if (Length < record.Length) return -1;
            if (Length > record.Length) return 1;
            return 0;
        }
    }

    public class MinHeapTest
    {
        [Test]
        public void GivenElement_ShouldAddToHeap()
        {
            var heap = new MinHeap<TestRecord>();
            var record = new TestRecord(1, 2);
            heap.Insert(record);
            Assert.AreEqual(new[] {record}, heap.ToArray());
        }

        [Test]
        public void InsertElement_ToExistingHeap_ShouldInsertAndMaintainHeapStructure()
        {
            var heap = new MinHeap<TestRecord>();
            var record1 = new TestRecord(1, 2);
            var record2 = new TestRecord(2, 1);
            heap.Insert(record1);
            heap.Insert(record2);
            Assert.AreEqual(new[] {record2 with{}, record1 with{}}, heap.ToArray());
        }

        [Test]
        public void Pull_ShouldRemoveRootElementAndMaintainHeap()
        {
            var heap = new MinHeap<TestRecord>();
            var record1 = new TestRecord(1, 2);
            var record2 = new TestRecord(2, 1);
            var record3 = new TestRecord(3, 3);
            heap.Insert(record1);
            heap.Insert(record2);
            heap.Insert(record3);
            var record = heap.Pull();
            Assert.AreEqual(new TestRecord(2, 1), record);
            Assert.AreEqual(new List<TestRecord> {new TestRecord(1, 2), new TestRecord(3, 3)}, heap.ToArray());
        }

        [Test]
        public void PullOnEmptyHeapShouldReturnNull()
        {
            var heap = new MinHeap<TestRecord>();
            var min = heap.Pull();
            Assert.Null(min);
        }

        [Test]
        public void GivenCollectionOfElements_ShouldHeapify()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MinHeap<TestRecord>();
            heap.InsertMany(records);
            Assert.AreEqual(new List<TestRecord>
            {
                new(2, 3), new(1, 5), new(3, 4),
            }, heap.ToArray());
        }

        [Test]
        public void ShouldDeleteElementFromHeap()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MinHeap<TestRecord>();
            heap.InsertMany(records);
            var result = heap.TryDelete(record => record.Id == 3);
            Assert.True(result);
            Assert.AreEqual(new List<TestRecord>
            {
                new(2, 3), new(1, 5)
            }, heap.ToArray());
        }

        [Test]
        public void DeleteNonExisitItemShouldReturnFalse()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MinHeap<TestRecord>();
            heap.InsertMany(records);
            var result = heap.TryDelete(record => record.Id == 4);
            Assert.False(result);
            Assert.AreEqual(new List<TestRecord>
            {
                new(2, 3), new(1, 5), new(3, 4)
            }, heap.ToArray());
        }

        [Test]
        public void ShouldHeapifyLargerCollection()
        {
            var records = new List<TestRecord>
            {
                new(8, 8),
                new(4, 4),
                new(5, 5),
                new(9, 9),
                new(12, 12),
                new(15, 15),
                new(3, 3),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(7, 7),
                new(7, 7)
            };
            var heap = new MinHeap<TestRecord>(records);

            Assert.AreEqual(new List<TestRecord>
            {
                new(1, 1),
                new(2, 2),
                new(4, 4),
                new(3, 3),
                new(6, 6),
                new(7, 7),
                new(5, 5),
                new(9, 9),
                new(8, 8),
                new(12, 12),
                new(7, 7),
                new(15, 15),
            }, heap.ToArray());
            heap.TryDelete(x => x.Id == 3);
            Assert.AreEqual(new List<TestRecord>
            {
                new(1, 1),
                new(2, 2),
                new(4, 4),
                new(8, 8),
                new(6, 6),
                new(7, 7),
                new(5, 5),
                new(9, 9),
                new(15, 15),
                new(12, 12),
                new(7, 7),
            }, heap.ToArray());
            var min = heap.Pull();
            Assert.AreEqual(new TestRecord(1, 1), min);
            Assert.AreEqual(new List<TestRecord>
            {
                new(2, 2),
                new(6, 6),
                new(4, 4),
                new(8, 8),
                new(7, 7),
                new(7, 7),
                new(5, 5),
                new(9, 9),
                new(15, 15),
                new(12, 12),
            }, heap.ToArray());
            heap.Insert(new TestRecord(15, 9));
            Assert.AreEqual(new List<TestRecord>
            {
                new(2, 2),
                new(6, 6),
                new(4, 4),
                new(8, 8),
                new(7, 7),
                new(7, 7),
                new(5, 5),
                new(9, 9),
                new(15, 15),
                new(12, 12),
                new(15, 9)
            }, heap.ToArray());
            heap.Insert(new TestRecord(15, 1));
            Assert.AreEqual(new List<TestRecord>
            {
                new TestRecord(15, 1),
                new(6, 6),
                new(2, 2),
                new(8, 8),
                new(7, 7),
                new(4, 4),
                new(5, 5),
                new(9, 9),
                new(15, 15),
                new(12, 12),
                new(15, 9),
                new(7, 7),
            }, heap.ToArray());
        }
    }

    public class MaxHeapTest
    {
        [Test]
        public void GivenElement_ShouldAddToHeap()
        {
            var heap = new MaxHeap<TestRecord>();
            var record = new TestRecord(1, 2);
            heap.Insert(record);
            Assert.AreEqual(new[] {record}, heap.ToArray());
        }

        [Test]
        public void InsertElement_ToExistingHeap_ShouldInsertAndMaintainHeapStructure()
        {
            var heap = new MaxHeap<TestRecord>();
            var record1 = new TestRecord(1, 2);
            var record2 = new TestRecord(2, 1);
            heap.Insert(record1);
            heap.Insert(record2);
            Assert.AreEqual(new[] {record1 with{}, record2 with{}}, heap.ToArray());
        }

        [Test]
        public void Pull_ShouldRemoveRootElementAndMaintainHeap()
        {
            var heap = new MaxHeap<TestRecord>();
            var record1 = new TestRecord(1, 2);
            var record2 = new TestRecord(2, 1);
            var record3 = new TestRecord(3, 3);
            heap.Insert(record1);
            heap.Insert(record2);
            heap.Insert(record3);
            var record = heap.Pull();
            Assert.AreEqual(new TestRecord(3, 3), record);
            Assert.AreEqual(new List<TestRecord> {new(1, 2), new(2, 1)}, heap.ToArray());
        }

        [Test]
        public void PullOnEmptyHeapShouldReturnNull()
        {
            var heap = new MaxHeap<TestRecord>();
            var min = heap.Pull();
            Assert.Null(min);
        }

        [Test]
        public void GivenCollectionOfElements_ShouldHeapify()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MaxHeap<TestRecord>();
            heap.InsertMany(records);
            Assert.AreEqual(new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4),
            }, heap.ToArray());
        }

        [Test]
        public void ShouldDeleteElementFromHeap()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MaxHeap<TestRecord>();
            heap.InsertMany(records);
            var result = heap.TryDelete(record => record.Id == 3);
            Assert.True(result);
            Assert.AreEqual(new List<TestRecord>
            {
                new(1, 5), new(2, 3)
            }, heap.ToArray());
        }

        [Test]
        public void DeleteNonExisitItemShouldReturnFalse()
        {
            var records = new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            };
            var heap = new MaxHeap<TestRecord>();
            heap.InsertMany(records);
            var result = heap.TryDelete(record => record.Id == 4);
            Assert.False(result);
            Assert.AreEqual(new List<TestRecord>
            {
                new(1, 5), new(2, 3), new(3, 4)
            }, heap.ToArray());
        }

        [Test]
        public void ShouldHeapifyLargerCollection()
        {
            var records = new List<TestRecord>
            {
                new(8, 8),
                new(4, 4),
                new(5, 5),
                new(9, 9),
                new(12, 12),
                new(15, 15),
                new(3, 3),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(7, 7),
                new(7, 7)
            };
            var heap = new MaxHeap<TestRecord>(records);

            Assert.AreEqual(new List<TestRecord>
            {
                new(15, 15),
                new(9, 9),
                new(12, 12),
                new(4, 4),
                new(8, 8),
                new(7, 7),
                new(3, 3),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(7, 7),
                new(5, 5),
            }, heap.ToArray());
            heap.TryDelete(x => x.Id == 3);
            Assert.AreEqual(new List<TestRecord>
            {
                new(15, 15),
                new(9, 9),
                new(12, 12),
                new(4, 4),
                new(8, 8),
                new(7, 7),
                new(5, 5),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(7, 7),
            }, heap.ToArray());
            var min = heap.Pull();
            Assert.AreEqual(new TestRecord(15, 15), min);
            Assert.AreEqual(new List<TestRecord>
            {
                new(12, 12),
                new(9, 9),
                new(7, 7),
                new(4, 4),
                new(8, 8),
                new(7, 7),
                new(5, 5),
                new(1, 1),
                new(2, 2),
                new(6, 6),
            }, heap.ToArray());
            heap.Insert(new TestRecord(15, 9));
            Assert.AreEqual(new List<TestRecord>
            {
                new(12, 12),
                new(9, 9),
                new(7, 7),
                new(4, 4),
                new TestRecord(15, 9),
                new(7, 7),
                new(5, 5),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(8, 8),
            }, heap.ToArray());
            heap.Insert(new TestRecord(15, 11));
            Assert.AreEqual(new List<TestRecord>
            {
                new(12, 12),
                new(9, 9),
                new TestRecord(15, 11),
                new(4, 4),
                new TestRecord(15, 9),
                new(7, 7),
                new(5, 5),
                new(1, 1),
                new(2, 2),
                new(6, 6),
                new(8, 8),
                new(7, 7),
            }, heap.ToArray());
        }
    }
}