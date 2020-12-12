using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentFour
{
    public class TwoSumTest
    {
        private readonly long[] _input = new[]
        {
            68037543430,
            68037543431,
            -68037543123,
            0,
            -10000,
            15493,
            5000,
            5000
        };
        
        private Dictionary<long, long[]> _dictionary = new Dictionary<long, long[]>
        {
            {6803754, new[] {68037543430, 68037543431, -68037543123}},
            {0, new[] {0L, 5000L, 5000L}},
            {1, new[] {-10000L, 15493L}},
        };
        
        [Test]
        public void GivenTestArray_ShouldBucketThemIntoTenThousands()
        {
            var sut = new TwoSum(_input);
            Assert.AreEqual(_dictionary, sut.Input);
        }
        
        [Test]
        public void GivenGroupedDict_ShouldCountTwoSumBetween_Negative10000_10000()
        {
            var sut = new TwoSum(_input);
            sut.GetAllCount();
        
            Assert.AreEqual(new HashSet<int> {307, 308, 5000, 10000, 5493},
                sut.HasTwoSum);
        }
        
        [Test]
        public void GivenTestInput_ShouldReturnCount5()
        {
            var sut = new TwoSum(_input);
            Assert.AreEqual(5, sut.GetAllCount());
        }

        [Test]
        public void GivenTestArray_ShouldCountTwoSumCounts()
        {
            var input = new FileReader().ReadFile("AssignmentFour", "2sum.txt")
                .Select(long.Parse);
            var sut = new TwoSum(input.ToArray());
            Assert.AreEqual(414, sut.GetAllCount());
        }
    }
}