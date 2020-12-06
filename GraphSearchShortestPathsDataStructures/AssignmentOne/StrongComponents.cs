using System.Collections.Generic;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class Vertex
    {
        public int OriginalNo { get; }
        public List<Vertex> Heads { get; }
        public List<Vertex> Tails { get; }
        public bool IsVisited { get; private set; }
        public int FinishTime { get; set; }
        public Vertex? Leader { get; }

        public Vertex(int originalNo,  List<Vertex> heads, List<Vertex> tails, bool isVisited, int finishTime, Vertex? leader)
        {
            OriginalNo = originalNo;
            Heads = heads;
            Tails = tails;
            IsVisited = isVisited;
            FinishTime = finishTime;
            Leader = leader;
        }

        public void MarkIsVisited()
        {
            IsVisited = true;
        }
    };

    public class StrongComponents
    {
        private readonly Dictionary<int, Vertex> _graph;
        private int _finishingTime = 0;
        private Vertex? _lead;

        public StrongComponents(Dictionary<int, Vertex> graph)
        {
            _graph = graph;
        }

        public void FirstReverseSearch()
        {
            for (int i = _graph.Count; i >= 1; i--)
            {
                if (_graph[i].IsVisited)
                {
                    continue;
                }
                DepthFirstSearch(_graph[i]);
            }
        }

        public void DepthFirstSearch(Vertex leadVertex)
        {
            leadVertex.MarkIsVisited();
            _lead = leadVertex;
            foreach (var vertex in leadVertex.Tails)
            {
                if (vertex.IsVisited) continue;
                DepthFirstSearch(vertex);
            }

            _finishingTime++;
            leadVertex.FinishTime = _finishingTime;
        }

        public void FinishTimeToKey()
        {
            throw new System.NotImplementedException();
        }
    }
}