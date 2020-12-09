using System.Collections.Generic;
using System.Linq;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class StrongComponents
    {
        private Dictionary<int, bool> IsDiscovered { get; set; } = new();
        private Dictionary<int, bool> IsInStack { get; set; } = new();
        public Dictionary<int, int> LeadTracker { get; private set; } = new();
        public Dictionary<int, int> FinishingTimeTracker { get; private set; } = new();
        private int _finishingTime;
        private int _lead;

        public int[] GetStrongComponents(Dictionary<int, int[]> graph, Dictionary<int, int[]> reversedGraph)
        {
            SearchGraph(reversedGraph);
            var updatedGraph = ReplaceVertexWithFinishTime(graph);
            SearchGraph(updatedGraph);
            return GetFiveStrongComponentsCounts();
        }

        public void SearchGraph(Dictionary<int, int[]> graph)
        {
            ResetTrackers(graph);
            for (var i = graph.Count; i >= 1; i--)
            {
                if (IsDiscovered[i])
                    continue;

                DepthFirstSearch(graph, i);
            }
        }

        private void ResetTrackers(Dictionary<int, int[]> graph)
        {
            _finishingTime = 0;
            _lead = 0;

            IsDiscovered = graph.ToDictionary(e => e.Key, e => false);
            IsInStack = graph.ToDictionary(e => e.Key, e => false);
            LeadTracker = new Dictionary<int, int>();
            FinishingTimeTracker = new Dictionary<int, int>();
        }

        private void DepthFirstSearch(Dictionary<int, int[]> graph, int lead)
        {
            _lead = lead;
            var stack = new Stack<int>();
            AddVertexToStack(lead, stack);
            while (stack.Any())
            {
                var top = stack.Peek();
                if (!IsDiscovered[top])
                {
                    IsDiscovered[top] = true;
                    foreach (var adjacentVertex in graph[top])
                    {
                        if (IsDiscovered[adjacentVertex] || IsInStack[adjacentVertex]) continue;
                        AddVertexToStack(adjacentVertex, stack);
                    }

                    continue;
                }

                stack.Pop();
                UpdateTrackers(top);
            }
        }

        private void AddVertexToStack(int vertex, Stack<int> stack)
        {
            stack.Push(vertex);
            IsInStack[vertex] = true;
        }

        private void UpdateTrackers(int top)
        {
            LeadTracker.Add(top, _lead);
            _finishingTime++;
            FinishingTimeTracker.Add(top, _finishingTime);
        }

        public Dictionary<int, int[]> ReplaceVertexWithFinishTime(Dictionary<int, int[]> graph)
        {
            return graph.ToDictionary(e => FinishingTimeTracker[e.Key],
                e => e.Value.Select(x => FinishingTimeTracker[x]).ToArray());
        }

        private int[] GetFiveStrongComponentsCounts()
        {
            return LeadTracker.GroupBy(x => x.Value).Select(x => x.Count())
                .OrderByDescending(x => x).Take(5).ToArray();
        }
    }
}