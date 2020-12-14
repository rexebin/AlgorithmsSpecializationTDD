using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentFour
{
    public class TwoSumTest
    {
        private readonly long[] _input =
        {
            68037543430,
            68037543431,
            -68037543123,
            0,
            -10000,
            15493,
            8000,
            5000,
            5000
        };

        [Test]
        public void GivenInputShouldGenerateSortedDictionary()
        {
            var sut = new TwoSum(_input);
            Assert.AreEqual(new SortedSet<long>
            {
                -68037543123,
                -10000,
                0,
                5000,
                8000,
                15493,
                68037543430,
                68037543431,
            }, sut.UniqueNumbers);
            Assert.AreEqual(new HashSet<long>{5000}, sut.Duplications);
            
        }

        [Test]
        public void GivenTestInput_ShouldReturnTwoSumCount()
        {
            var sut = new TwoSum(_input);
            var result = sut.GetCount();
            Assert.AreEqual(9, result); //307, 308, 5000, -10000, 5493, -5000, 8000, -2000, 10000
        }

        [Test]
        public void GivenAssignmentArray_GetResult()
        {
            var input = new FileReader()
                .ReadFile("AssignmentFour", "2sum.txt")
                .Select(long.Parse);
            var sut = new TwoSum(input.ToArray());
            var result = sut.GetCount();
            Assert.AreEqual(427, result);
        }
    }
}