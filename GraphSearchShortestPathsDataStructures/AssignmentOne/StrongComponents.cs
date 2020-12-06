using System.Collections.Generic;
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
                if (Status[i])
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
                if (Status[vertex])
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
                if (FinishingTimes.TryGetValue(i, out var vertex))
                {
                    newGraph.Add(vertex, originalGraph[i].Select(x => FinishingTimes[x]).ToArray());
                }
            }

            return newGraph;
            // graph = graph.ToDictionary(entry => entry.Value.FinishTime,
            //     entry =>
            //     {
            //         entry.Value.IsVisited = false;
            //         return entry.Value;
            //     });
        }

        public int[] GetStrongComponentsCounts()
        {
            return Leads.GroupBy(x => x.Value).Select(x => x.Count()).ToArray();
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


        // public static Dictionary<int, Vertex> TransformToGraph(int[][] array)
        // {
        //     var groupByTails = GroupByTails(array);
        //     var groupByHeads = GroupByHeads(array);
        //     var merged = groupByTails.ToDictionary(e => e.Key,
        //         e =>
        //             new Vertex(e.Key, new List<Vertex>(), new List<Vertex>(), false, 0, null));
        //     foreach (var heads in groupByHeads.Where(keyValuePair => !merged.TryGetValue(keyValuePair.Key, out _)))
        //     {
        //         merged.TryAdd(heads.Key,
        //             new Vertex(heads.Key, new List<Vertex>(),
        //                 new List<Vertex>(), false, 0, null));
        //     }
        //
        //     foreach (var vertex in merged)
        //     {
        //         if (groupByTails.TryGetValue(vertex.Key, out var allHeads))
        //         {
        //             foreach (var headNo in allHeads)
        //             {
        //                 if (merged.TryGetValue(headNo, out var head))
        //                     vertex.Value.Heads.Add(head);
        //             }
        //         }
        //
        //         if (groupByHeads.TryGetValue(vertex.Key, out var allTails))
        //         {
        //             foreach (var tailNo in allTails)
        //             {
        //                 if (merged.TryGetValue(tailNo, out var tail))
        //                     vertex.Value.Tails.Add(tail);
        //             }
        //         }
        //     }
        //
        //     return merged;
        // }
        public void GetStrongComponents(int[][] fileInput)
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
        }
    }
}