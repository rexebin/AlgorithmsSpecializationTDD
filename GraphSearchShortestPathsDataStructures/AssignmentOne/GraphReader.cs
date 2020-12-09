using System;
using System.Collections.Generic;
using System.Linq;
using Utility.Common;

namespace GraphSearchShortestPathsDataStructures.AssignmentOne
{
    public static class GraphReader
    {
        public static int[][] ReadFile(string filename)
        {
            return new FileReader()
                .ReadFile("AssignmentOne", filename)
                .Select(x => x.TrimEnd().Split(' ')
                    .Select(int.Parse)
                    .ToArray()).ToArray();
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

        public static Dictionary<int, int[]> AddMissingVertices(Dictionary<int, int[]> graph)
        {
            for (var i = 1; i <= 875714; i++)
            {
                if (graph.TryGetValue(i, out _))
                {
                    continue;
                }

                graph.Add(i, Array.Empty<int>());
            }

            return graph;
        }
    }
}