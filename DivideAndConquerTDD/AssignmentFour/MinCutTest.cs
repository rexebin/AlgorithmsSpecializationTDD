using System.Collections.Generic;
using NUnit.Framework;

namespace DivideAndConquerTDD.AssignmentFour
{
    public class MinCutTest
    {
        [Test]
        public void ShouldGetRandomKeyVertex()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {1, 3}},
                {3, new List<int> {1, 2}}
            };
            var sut = new MinCut(graph);
            var result = sut.GetRandomKeyVertex();
            Assert.Contains(result, new[] {1, 2, 3});
        }

        [Test]
        public void ShouldGetRandomTargetVertex()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3, 4, 5, 6, 7, 8, 9}}
            };
            var sut = new MinCut(graph);
            var result = sut.GetRandomTargetVertex(1);
            Assert.Contains(result, new[] {2, 3, 4, 5, 6, 7, 8, 9});
        }

        [Test]
        public void ShouldMergeKeyValuesToTargetValues()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {1, 3}},
                {3, new List<int> {1, 2}}
            };
            var sut = new MinCut(graph);
            sut.MergeKeyValuesToTargetValues(1, 2);
            var result = sut.GetGraph();
            var expected = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {1, 3, 2, 3}},
                {3, new List<int> {1, 2}}
            };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldReplaceKeyVertex1WithTargetVertex2()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {1, 3, 2, 3}},
                {3, new List<int> {1, 2}}
            };
            var sut = new MinCut(graph);
            sut.ReplaceKeyVertexWithTargetVertex(1, 2);
            var result = sut.GetGraph();
            var expected = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {2, 3, 2, 3}},
                {3, new List<int> {2, 2}}
            };
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void ShouldRemoveKeyVertex()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 3}},
                {2, new List<int> {2, 3, 2, 3}},
                {3, new List<int> {2, 2}}
            };
            var sut = new MinCut(graph);
            sut.RemoveVertex(1);
            var result = sut.GetGraph();
            var expected = new Dictionary<int, List<int>>
            {
                {2, new List<int> {2, 3, 2, 3}},
                {3, new List<int> {2, 2}}
            };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldRemoveLeftLoop()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {2, new List<int> {2, 3, 2, 3}},
                {3, new List<int> {2, 2}}
            };
            var sut = new MinCut(graph);
            sut.RemoveSelfLoop(2);
            var result = sut.GetGraph();
            var expected = new Dictionary<int, List<int>>
            {
                {2, new List<int> {3, 3}},
                {3, new List<int> {2, 2}}
            };
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldReturnMinCut()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {2, new List<int> {3, 3}},
                {3, new List<int> {2, 2}}
            };
            var sut = new MinCut(graph);
            var result = sut.GetMinCut();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GivenTwoVerticesGraph_ShouldCalculateMinCut()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2}},
                {2, new List<int> {1}}
            };
            var sut = new MinCut(graph);
            var result = sut.GetMinCut();
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GivenFourVerticesGraph_ShouldCalculateMinCut()
        {
            var graph = new Dictionary<int, List<int>>
            {
                {1, new List<int> {2, 4}},
                {2, new List<int> {1, 3, 4}},
                {3, new List<int> {2, 4}},
                {4, new List<int> {1, 2, 3}}
            };
            var sut = new MinCut(graph);
            var result = sut.GetMinCut();
            Assert.AreEqual(2, result);
        }

        [Test]
        public void ShouldReadMinCutTestFileToDictionary()
        {
            var result = MinCut.ReadFile();
            Assert.AreEqual(200, result.Count);
        }

        [Test]
        public void ShouldCalcMinCutOfGivenText()
        {
            var sut = new MinCut(MinCut.ReadFile());
            var result = sut.GetMinCut();
            Assert.AreEqual(17, result);
        }
    }
}