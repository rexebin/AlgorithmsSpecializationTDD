using System;
using System.Collections.Generic;
using System.Linq;

namespace DivideAndConquerTDD
{
    public class FindSecondLargestNumber
    {
        private int _compareCount;

        private readonly Dictionary<int, List<int>> _smallerNumbers = new();

        public int GetSecondLargestNumber(int[] numbers)
        {
            var topLevelLargestPair = GetTopLevelLargestPair(numbers);
            Compare(topLevelLargestPair[0], topLevelLargestPair[1],
                out var largestNumber, out var secondLargestNumber);
            var largestNumbersInSmallerSubNumbers = GetLargestNumbersInSmallerSubNumbers(largestNumber);
            _compareCount++;
            return secondLargestNumber > largestNumbersInSmallerSubNumbers
                ? secondLargestNumber
                : largestNumbersInSmallerSubNumbers;
        }

        private int[] GetTopLevelLargestPair(int[] numbers)
        {
            while (numbers.Length > 2)
            {
                var halfLength = numbers.Length / 2;
                var firstHalf = numbers.Take(halfLength).ToArray();
                var secondHalf = numbers.Skip(halfLength).Take(numbers.Length - halfLength).ToArray();
                numbers = GetLargeNumbers(firstHalf, secondHalf);
            }

            return numbers;
        }

        private int[] GetLargeNumbers(int[] numbers1, int[] numbers2)
        {
            return numbers1.Select((t, i) => GetLargerNumber(t, numbers2[i])).ToArray();
        }

        private int GetLargerNumber(int number1, int number2)
        {
            Compare(number1, number2, out var larger, out var smaller);
            SaveSmallNumber(larger, smaller);
            return larger;
        }

        private int GetLargestNumbersInSmallerSubNumbers(int largestNumber)
        {
            var smallerNumbers = _smallerNumbers.GetValueOrDefault(largestNumber);
            if (smallerNumbers == null) throw new Exception("Largest number's compared numbers not saved!");
            _compareCount += smallerNumbers.Count - 1;
            return smallerNumbers.Max();
        }

        private void Compare(int number1, int number2, out int larger, out int smaller)
        {
            _compareCount++;
            if (number1 > number2)
            {
                larger = number1;
                smaller = number2;
            }
            else
            {
                larger = number2;
                smaller = number1;
            }
        }


        private void SaveSmallNumber(int largerNumber, int smallerNumber)
        {
            if (_smallerNumbers.TryGetValue(largerNumber, out var smallerNumbers))
            {
                smallerNumbers.Add(smallerNumber);
                return;
            }

            _smallerNumbers.Add(largerNumber, new List<int> {smallerNumber});
        }


        public bool IsCompareCountWithinConstraint(int[] numbers)
        {
            //n+log2n-2;
            var n = numbers.Length;
            return _compareCount <= n + Math.Log2(n) - 2;
        }
    }
}