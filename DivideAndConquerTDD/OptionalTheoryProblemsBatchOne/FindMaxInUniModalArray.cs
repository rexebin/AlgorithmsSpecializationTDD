using System.Linq;

namespace DivideAndConquerTDD.OptionalTheoryProblemsBatchOne
{
    public class FindMaxInUniModalArray
    {
        public int[] GetPeakPart(int[] numbers)
        {
            var halfLength = numbers.Length / 2;
            return numbers[halfLength - 1] > numbers[halfLength]
                ? numbers.Take(halfLength).ToArray()
                : numbers.Skip(halfLength).Take(numbers.Length - halfLength).ToArray();
        }

        public int GetMax(int[] numbers)
        {
            while (numbers.Length > 2)
            {
                numbers = GetPeakPart(numbers);
            }
            return numbers[0] > numbers[1] ? numbers[0] : numbers[1];
        }
    }
}