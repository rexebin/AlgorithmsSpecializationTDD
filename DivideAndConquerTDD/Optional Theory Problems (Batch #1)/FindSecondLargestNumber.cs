using System;
using System.Linq;

namespace DivideAndConquerTDD
{
    public class FindSecondLargestNumber
    {
        private int _compareCount;


        public int GetSecondLargestNumber(int[] numbers)
        {
            while (numbers.Length > 2)
            {
                var halfLength = numbers.Length / 2;
                var firstHalf = numbers.Take(halfLength).ToArray();
                var secondHalf = numbers.Skip(halfLength).Take(numbers.Length - halfLength).ToArray();
                numbers = GetLargeNumbers(firstHalf, secondHalf);
            }

            return numbers[0] > numbers[1] ? numbers[1] : numbers[0];
        }

        public int[] GetLargeNumbers(int[] numbers1, int[] numbers2)
        {
            return numbers1.Select((t, i) => GetLargerNumber(t, numbers2[i])).ToArray();
        }

        private int GetLargerNumber(int number1, int number2)
        {
            _compareCount++;
            return number1 > number2 ? number1 : number2;
        }

        public bool IsCompareCountInConstraint(int[] numbers)
        {
            //n+log2n-2;
            var n = numbers.Length;
            return _compareCount < n + Math.Log2(n) - 2;
        }
    }
}