using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class StrongComponents
    {
        private Dictionary<int, bool> Status { get; set; } = new();
        public Dictionary<int, int> Leads { get; private set; } = new();
        public Dictionary<int, int> FinishingTimes { get; private set; } = new();
        private int _finishingTime;
        private int _lead;

        public static int[][] ReadFile()
        {
            return new FileReader()
                .ReadFile("AssignmentOne", "SCC.txt")
                .Select(x => x.TrimEnd().Split(' ')
                    .Select(int.Parse)
                    .ToArray()).ToArray();
        }

        public int[] GetStrongComponents(int[][] fileInput)
        {
            var reversedGraph = GetReversedGraph(fileInput);
            reversedGraph = AddMissingVertices(reversedGraph);
            DepthFirstSearch(reversedGraph);
            var graph = GetGraph(fileInput);
            graph = AddMissingVertices(graph);
            graph = ReplaceVertexWithFinishTime(graph);
            DepthFirstSearch(graph);
            return GetFiveStrongComponentsCounts();
        }

        private static Dictionary<int, int[]> AddMissingVertices(Dictionary<int, int[]> graph)
        {
            for (var i = 1; i <= 875714; i++)
            {
                if (graph.TryGetValue(i, out _)) continue;
                graph.Add(i, Array.Empty<int>());
            }

            return graph;
        }

        public void DepthFirstSearch(Dictionary<int, int[]> graph)
        {
            ResetTrackers(graph);
            for (var i = graph.Count; i >= 1; i--)
            {
                if (Status[i])
                    continue;

                _lead = i;
                DepthFirstSearch(graph, i);
            }
        }

        private void ResetTrackers(Dictionary<int, int[]> graph)
        {
            _finishingTime = 0;
            _lead = 0;

            Status = graph.ToDictionary(e => e.Key, e => false);
            Leads = new Dictionary<int, int>();
            FinishingTimes = new Dictionary<int, int>();
        }

        private void DepthFirstSearch(Dictionary<int, int[]> graph, int vertex)
        {
            Status[vertex] = true;
            foreach (var nextVertex in graph[vertex])
            {
                if (Status[nextVertex])
                    continue;

                DepthFirstSearch(graph, nextVertex);
            }

            Leads[vertex] = _lead;
            _finishingTime++;
            FinishingTimes[vertex] = _finishingTime;
        }

        public static Dictionary<int, int[]> GetGraph(IEnumerable<int[]> edges)
        {
            return edges.GroupBy(x => x.First())
                .ToDictionary(e => e.Key,
                    e => e.Select(x => x.Last()).ToArray());
        }

        public static Dictionary<int, int[]> GetReversedGraph(IEnumerable<int[]> edges)
        {
            return edges.GroupBy(x => x.Last())
                .ToDictionary(e => e.Key,
                    e => e.Select(x => x.First()).ToArray());
        }

        public Dictionary<int, int[]> ReplaceVertexWithFinishTime(Dictionary<int, int[]> graph)
        {
            var result = new Dictionary<int, int[]>();
            foreach (var (key, _) in graph)
            {
                if (!FinishingTimes.TryGetValue(key, out var finishingTime) || finishingTime == 0) continue;
                if (graph.TryGetValue(key, out var originalValue))
                {
                    result.Add(finishingTime, originalValue.Select(x => FinishingTimes[x]).ToArray());
                }
            }

            return result;
        }

        public int[] GetFiveStrongComponentsCounts()
        {
            return Leads.GroupBy(x => x.Value).Select(x => x.Count())
                .OrderByDescending(x => x).Take(5).ToArray();
        }
    }
}