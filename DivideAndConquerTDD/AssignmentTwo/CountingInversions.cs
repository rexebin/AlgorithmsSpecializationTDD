using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace DivideAndConquerTDD.AssignmentTwo
{
    public class CountingInversions
    {
        private long _inversions;

        public int[] SortAndCountInversions(int[] input)
        {
            if (input.Length == 1)
                return input;

            var length = input.Length;
            var left = SortAndCountInversions(input.Take(length / 2).ToArray());
            var right = SortAndCountInversions(input.Skip(length / 2).Take(length - length / 2).ToArray());
            return MergeAndSort(left, right);
        }

        public int[] SortAndCountInversions()
        {
            var input = File.ReadAllLines(GetPath())
                .Select(int.Parse).ToArray();

            if (input.Length == 1)
                return input;

            var length = input.Length;
            var left = SortAndCountInversions(input.Take(length / 2).ToArray());
            var right = SortAndCountInversions(input.Skip(length / 2).Take(length - length / 2).ToArray());
            return MergeAndSort(left, right);
        }

        public long GetInversions()
        {
            return _inversions;
        }

        public int[] MergeAndSort(int[] arrayB, int[] arrayC)
        {
            var mergedArray = new List<int>();
            var j = 0;
            var i = 0;
            while (i < arrayB.Length)
            {
                if (j == arrayC.Length)
                {
                    mergedArray.Add(arrayB[i]);
                    i++;
                    continue;
                }

                if (arrayB[i] < arrayC[j])
                {
                    mergedArray.Add(arrayB[i]);
                    i++;
                }
                else
                {
                    mergedArray.Add(arrayC[j]);
                    _inversions += arrayB.Length - i;
                    j++;
                }
            }

            while (j < arrayC.Length)
            {
                mergedArray.Add(arrayC[j]);
                j++;
            }


            return mergedArray.ToArray();
        }

        public string GetPath()
        {
            return Path.GetFullPath(@"..\..\..\AssignmentTwo\IntegerArray.txt");
        }
    }
}