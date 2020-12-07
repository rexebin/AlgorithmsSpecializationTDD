using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public class Vertex
    {
        public int OriginalNo { get; }
        public List<Vertex> Heads { get; }
        public List<Vertex> Tails { get; }
        public bool IsVisited { get; set; }
        public int FinishTime { get; set; }
        public Vertex? Leader { get; set; }

        public Vertex(int originalNo, List<Vertex> heads, List<Vertex> tails, bool isVisited, int finishTime,
            Vertex? leader)
        {
            OriginalNo = originalNo;
            Heads = heads;
            Tails = tails;
            IsVisited = isVisited;
            FinishTime = finishTime;
            Leader = leader;
        }
    };

    public class StrongComponents
    {
        public Dictionary<int, bool> Status { get; set; }
        public Dictionary<int, int> Leads { get; set; }
        public Dictionary<int, int> FinishingTimes { get; set; }
        private int _finishingTime;
        private int _lead;

        public void DepthFirstSearch(Dictionary<int, int[]> graph)
        {
            _finishingTime = 0;
            _lead = 0;

            Status = graph.ToDictionary(e => e.Key, e => false);
            Leads = graph.ToDictionary(e => e.Key, e => 0);
            FinishingTimes = graph.ToDictionary(e => e.Key, e => 0);
            for (var i = graph.Count; i >= 1; i--)
            {
                if (Status.TryGetValue(i, out var s) && s)
                {
                    continue;
                }

                _lead = i;
                DepthFirstSearch(graph, i);
            }
        }

        private void DepthFirstSearch(Dictionary<int, int[]> graph, int vertexToSearch)
        {
            Status[vertexToSearch] = true;
            foreach (var vertex in graph[vertexToSearch])
            {
                if (Status.TryGetValue(vertex, out var s) && s)
                {
                    continue;
                }

                DepthFirstSearch(graph, vertex);
            }

            Leads[vertexToSearch] = _lead;
            _finishingTime++;
            FinishingTimes[vertexToSearch] = _finishingTime;
        }

        public Dictionary<int, int[]> FinishTimeToKeyAndResetStatus(Dictionary<int, int[]> originalGraph)
        {
            var newGraph = new Dictionary<int, int[]>();
            for (int i = 1; i <= originalGraph.Count; i++)
            {
                if (FinishingTimes.TryGetValue(i, out var vertex) && vertex!=0)
                {
                    if (originalGraph.TryGetValue(i, out var originalValue))
                        newGraph.Add(vertex, originalValue.Select(x => FinishingTimes[x]).ToArray());
                }
                else
                {
                    throw new Exception($"Finishing times not available for: {i}");
                }
            }

            return newGraph;
        }

        public int[] GetStrongComponentsCounts()
        {
            return Leads.GroupBy(x => x.Value).Select(x => x.Count())
                .OrderByDescending(x => x).Take(5).ToArray();
        }

        public static int[][] ReadFile()
        {
            return new FileReader()
                .ReadFile("AssignmentOne", "SCC.txt")
                .Select(x => x.TrimEnd().Split(' ')
                    .Select(x => int.Parse(x))
                    .ToArray()).ToArray();
        }

        public static Dictionary<int, int[]> GroupByTails(int[][] array)
        {
            return array.GroupBy(x => x.First())
                .ToDictionary(e => e.Key,
                    e =>
                        e.Select(x => x.Last()).ToArray()
                );
        }

        public static Dictionary<int, int[]> GroupByHeads(int[][] array)
        {
            return array.GroupBy(x => x.Last())
                .ToDictionary(e => e.Key,
                    e =>
                        e.Select(x => x.First()).ToArray()
                );
        }
        
        public int[] GetStrongComponents(int[][] fileInput)
        {
            var reverseGraph = GroupByHeads(fileInput);
            for (int i = 1; i <= 875714; i++)
            {
                if (!reverseGraph.TryGetValue(i, out var _))
                {
                    reverseGraph.Add(i, new int[0]);
                }
            }

            DepthFirstSearch(reverseGraph);
            var graph = GroupByTails(fileInput);
            var newGraph = FinishTimeToKeyAndResetStatus(graph);
            for (int i = 1; i <= 875714; i++)
            {
                if (!newGraph.TryGetValue(i, out var _))
                {
                    newGraph.Add(i, new int[0]);
                }
            }

            DepthFirstSearch(newGraph);
            return GetStrongComponentsCounts();
        }
    }
}