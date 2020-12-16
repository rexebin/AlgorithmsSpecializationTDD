using System.Collections.Generic;
using System.Linq;

namespace GreedyAlgorithmsMinimumSpanningTreesAndDynamicProgramming.WeekOneJobSchedulerAndPrimsMST
{
    public class Scheduler
    {
        public List<List<double>> SortByWeightMinusLength(double[][] jobs)
        {
            return AddCombinedLength(GetSortedByDiffJobs(jobs));
        }

        private static List<List<double>> GetSortedByDiffJobs(double[][] jobs)
        {
            return jobs
                .Select(x =>
                    x.Append(x[0] - x[1])
                        .ToList())
                .OrderByDescending(x => x[2])
                .ThenByDescending(x => x[0])
                .ToList();
        }

        public List<List<double>> SortByWeightLengthRatio(double[][] jobs)
        {
            return AddCombinedLength(GetSortByWeightLengthRatio(jobs));
        }

        private static List<List<double>> GetSortByWeightLengthRatio(double[][] jobs)
        {
            return jobs
                .Select(x =>
                    x.Append(x[0] / x[1])
                        .ToList())
                .OrderByDescending(x => x[2])
                .ToList();
        }

        private static List<List<double>> AddCombinedLength(List<List<double>> sortedJobs)
        {
            var newJobs = new List<List<double>>();
            for (var index = 0; index < sortedJobs.Count; index++)
            {
                var job = sortedJobs[index];
                if (index == 0)
                    job.Add(job[1]);
                else
                    job.Add(job[1] + newJobs[index - 1][3]);

                newJobs.Add(job);
            }

            return newJobs;
        }

        public double GetWeightedCompletionTime(List<List<double>> jobs)
        {
            return jobs.Sum(x => x[0] * x[3]);
        }
    }
}