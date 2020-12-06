using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
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
        private Dictionary<int, Vertex> graph;
        private int _finishingTime;
        private Vertex? _lead;

        public StrongComponents(Dictionary<int, Vertex> graph)
        {
            this.graph = graph;
        }

        public Dictionary<int, Vertex> GetGraph()
        {
            return graph;
        }

        public void DepthFirstSearch(bool isReverse)
        {
            _finishingTime = 0;
            _lead = null;
            if (!isReverse)
            {
                foreach (var keyValuePair in graph)
                {
                    keyValuePair.Value.Tails.Clear();
                    keyValuePair.Value.FinishTime = 0;
                }
            }

            for (var i = graph.Count; i >= 1; i--)
            {
                if (graph[i].IsVisited)
                {
                    continue;
                }

                _lead = graph[i];
                DepthFirstSearch(graph[i], isReverse);
            }
        }

        private void DepthFirstSearch(Vertex vertexToSearch, bool isReverse)
        {
            vertexToSearch.IsVisited = true;
            foreach (var vertex in (isReverse ? vertexToSearch.Tails : vertexToSearch.Heads))
            {
                if (vertex.IsVisited)
                    continue;
                DepthFirstSearch(vertex, isReverse);
            }
            
            vertexToSearch.Leader = _lead;
            _finishingTime++;
            vertexToSearch.FinishTime = _finishingTime;
        }

        public void FinishTimeToKeyAndResetStatus()
        {
            // var newGraph = new Dictionary<int, Vertex>();
            // for (int i = 1; i <= graph.Count; i++)
            // {
            //     graph[i].IsVisited = false;
            //     newGraph.Add(graph[i].FinishTime, graph[i]);
            // }
            //
            // graph = newGraph;
            graph = graph.ToDictionary(entry => entry.Value.FinishTime,
                entry =>
                {
                    entry.Value.IsVisited = false;
                    return entry.Value;
                });
        }

        public int[] GetStrongComponentsCounts()
        {
            return graph.Select(x => x.Value)
                .GroupBy(x => x.Leader)
                .Select(x => x.Count())
                .OrderByDescending(x => x)
                .ToArray();
        }

        public static Dictionary<int, Vertex> ReadFile()
        {
            var array = new FileReader()
                .ReadFile("AssignmentOne", "SCC.txt")
                .Select(x => x.TrimEnd().Split(' ')
                    .Select(x => int.Parse(x))
                    .ToArray())
                .Where(x => x[0] != x[1]);
            var graph = TransformToGraph(array.ToArray());
            return graph;
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

        public static Dictionary<int, Vertex> TransformToGraph(int[][] array)
        {
            var groupByTails = GroupByTails(array);
            var groupByHeads = GroupByHeads(array);
            var merged = groupByTails.ToDictionary(e => e.Key,
                e =>
                    new Vertex(e.Key, new List<Vertex>(), new List<Vertex>(), false, 0, null));
            foreach (var heads in groupByHeads.Where(keyValuePair => !merged.TryGetValue(keyValuePair.Key, out _)))
            {
                merged.TryAdd(heads.Key,
                    new Vertex(heads.Key, new List<Vertex>(),
                        new List<Vertex>(), false, 0, null));
            }

            foreach (var vertex in merged)
            {
                if (groupByTails.TryGetValue(vertex.Key, out var allHeads))
                {
                    foreach (var headNo in allHeads)
                    {
                        if (merged.TryGetValue(headNo, out var head))
                            vertex.Value.Heads.Add(head);
                    }
                }

                if (groupByHeads.TryGetValue(vertex.Key, out var allTails))
                {
                    foreach (var tailNo in allTails)
                    {
                        if (merged.TryGetValue(tailNo, out var tail))
                            vertex.Value.Tails.Add(tail);
                    }
                }
            }

            return merged;
        }
    }
}