using System;
using System.Linq;

namespace DivideAndConquerTDD
{
    public class HasIndexEqualValueElement
    {
        private int _count;

        public bool ContainsValueEqualIndex(int[] numbers, int startIndex = 0)
        {
            _count++;
            var result = false;
            if (numbers.Length == 1)
                return numbers[0] == startIndex;

            var halfLength = numbers.Length / 2;
            if (numbers[halfLength - 1] == halfLength - 1 + startIndex) return true;
            if (numbers[halfLength - 1] > halfLength - 1 + startIndex)
            {
                result = ContainsValueEqualIndex(numbers.Take(halfLength).ToArray(), startIndex);
            }

            if (result || numbers[halfLength] == halfLength + startIndex) return true;
            if (numbers[halfLength] >= halfLength + startIndex) return false;
            result = ContainsValueEqualIndex(numbers.Skip(halfLength).Take(numbers.Length - halfLength)
                .ToArray(), halfLength + startIndex);

            return result;
        }

        public void PrintCount()
        {
            Console.WriteLine(_count);
        }
    }
}