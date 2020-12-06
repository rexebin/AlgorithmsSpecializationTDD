using System.Collections.Generic;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class StrongComponentTest
    {
        private Dictionary<int, Vertex> _graph = null!;
        private StrongComponents _sut = null!;
        [SetUp]
        public void Setup()
        {
            _graph = new Dictionary<int, Vertex>
            {
                {1, new Vertex(1,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {2, new Vertex(2,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {3, new Vertex(3,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {4, new Vertex(4,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {5, new Vertex(5,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {6, new Vertex(6,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {7, new Vertex(7,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {8, new Vertex(8,new List<Vertex>(), new List<Vertex>(), false, 0, null)},
                {9, new Vertex(9,new List<Vertex>(), new List<Vertex>(), false, 0, null)}
            };
            _graph[1].Heads.Add(_graph[4]);
            _graph[1].Tails.Add(_graph[7]);
            _graph[2].Heads.Add(_graph[8]);
            _graph[2].Tails.Add(_graph[5]);
            _graph[3].Heads.Add(_graph[6]);
            _graph[3].Tails.Add(_graph[9]);
            _graph[4].Heads.Add(_graph[7]);
            _graph[4].Tails.Add(_graph[1]);
            _graph[5].Heads.Add(_graph[2]);
            _graph[5].Tails.Add(_graph[8]);
            _graph[6].Tails.AddRange(new List<Vertex> {_graph[3], _graph[8]});
            _graph[6].Heads.Add(_graph[9]);
            _graph[7].Tails.AddRange(new List<Vertex> {_graph[4], _graph[9]});
            _graph[7].Heads.AddRange(new List<Vertex> {_graph[1]});
            _graph[8].Tails.AddRange(new List<Vertex> {_graph[2]});
            _graph[8].Heads.AddRange(new List<Vertex> {_graph[6], _graph[5]});
            _graph[9].Tails.AddRange(new List<Vertex> {_graph[6]});
            _graph[9].Heads.AddRange(new List<Vertex> {_graph[3], _graph[7]});
            _sut = new StrongComponents(_graph);
        }
        
        [Test]
        public void GivenGroupAndLeadVertex_ShouldMarkFinishingTime()
        {
            _sut.FirstReverseSearch();
            Assert.AreEqual(1, _graph[3].FinishTime);
            Assert.AreEqual(2, _graph[5].FinishTime);
            Assert.AreEqual(3, _graph[2].FinishTime);
            Assert.AreEqual(4, _graph[8].FinishTime);
            Assert.AreEqual(5, _graph[6].FinishTime);
            Assert.AreEqual(6, _graph[9].FinishTime);
            Assert.AreEqual(7, _graph[1].FinishTime);
            Assert.AreEqual(8, _graph[4].FinishTime);
            Assert.AreEqual(9, _graph[7].FinishTime);
        }

        [Test]
        public void GivenFirstReverseSearchResult_ShouldSetFinishTimeToKey()
        {
            _sut.FirstReverseSearch();
            _sut.FinishTimeToKey();
            Assert.AreEqual(1, _graph[7].OriginalNo);
            Assert.AreEqual(2, _graph[3].OriginalNo);
            Assert.AreEqual(3, _graph[1].OriginalNo);
            Assert.AreEqual(4, _graph[8].OriginalNo);
            Assert.AreEqual(5, _graph[2].OriginalNo);
            Assert.AreEqual(6, _graph[5].OriginalNo);
            Assert.AreEqual(7, _graph[9].OriginalNo);
            Assert.AreEqual(8, _graph[4].OriginalNo);
            Assert.AreEqual(9, _graph[6].OriginalNo);
        }
    }
}