using System;
using System.Collections.Generic;
using System.Linq;

namespace DivideAndConquerTDD.Karatsuba
{
    public class Karatsuba
    {
        private int _performanceCount;

        public void PrintKaratTimes()
        {
            Console.WriteLine("Karatcuba executed: {0} times", _performanceCount);
        }
        
        /**
         * Karatsuba in strings, for out of range large numbers.
         */

        public string KaratsubaCalc(string number1, string number2)
        {
            if (number1.Length < 2 || number2.Length < 2)
            {
                _performanceCount++;
                return (int.Parse(number1) * int.Parse(number2)).ToString();
            }

            if (number1.Length % 2 != 0 || number2.Length % 2 != 0)
            {
                return MakeEvenAndKaratsubaCalc(number1, number2);
            }

            var inputLength = number1.Length;
            _performanceCount++;
            return KaratsubaFinalSum(
                CalcAAndC(number1, number2),
                CalcAPlusBTimesCPlusD(number1, number2),
                CalcBAndD(number1, number2),
                inputLength);
        }

        public string CalcAAndC(string number1, string number2)
        {
            return KaratsubaCalc(Divide(number1)[0], Divide(number2)[0]);
        }

        public string CalcBAndD(string number1, string number2)
        {
            return KaratsubaCalc(Divide(number1)[1], Divide(number2)[1]);
        }

        public string CalcAPlusBTimesCPlusD(string number1, string number2)
        {
            var div1 = Divide(number1);
            var div2 = Divide(number2);
            return KaratsubaCalc(Add(div1[0], div1[1]), Add(div2[0], div2[1]));
        }

        private string KaratsubaFinalSum(string ac, string aPlusBTimesCPlusD, string bd, int n)
        {
            // result = 10 ^ n * (1) + 10 ^ n / 2 * ((3) - (1) - (2)) + (3)
            return Add(Add(AddTailingZerosByGivenAmount(ac, n),
                AddTailingZerosByGivenAmount(Subtract(Subtract(aPlusBTimesCPlusD, ac), bd), n / 2)), bd);
        }

        private string MakeEvenAndKaratsubaCalc(string number1, string number2)
        {
            var digit1 = number1.Length;
            var digit2 = number2.Length;
            var diff = digit1 - digit2;
            var larger = digit1 > digit2 ? number1 : number2;
            var smaller = digit1 > digit2 ? number2 : number1;
            larger = AddTailingZerosByGivenAmount(larger, 1);
            smaller = AddTailingZerosByGivenAmount(smaller, Math.Abs(diff) + 1);
            return RemoveTailingZerosByGivenAmount(KaratsubaCalc(larger, smaller), Math.Abs(diff) + 2);
        }

        private string AddTailingZerosByGivenAmount(string input, int amount)
        {
            for (var i = 0; i < amount; i++)
                input = input + '0';
            return input;
        }

        private string RemoveTailingZerosByGivenAmount(string input, int amount)
        {
            for (var i = 0; i < amount; i++)
                input = input.Substring(0, input.Length - 1);
            return input;
        }
        
        /**
         * Karatsuba in integers.
         */

        public int KaratsubaCalc(int number1, int number2)
        {
            if (number1 < 10 || number2 < 10)
                return number1 * number2;

            if (number1.ToString().Length % 2 != 0 || number2.ToString().Length % 2 != 0)
            {
                return MakeEvenAndKaratsubaCalc(number1, number2);
            }

            var inputLength = number1.ToString().Length;
            var result = KaratsubaFinalSum(CalcAAndC(number1, number2),
                CalcAPlusBTimesCPlusD(number1, number2), CalcBAndD(number1, number2), inputLength);
            _performanceCount++;
            return result;
        }

        private static int KaratsubaFinalSum(int ac, int aPlusBTimesCPlusD, int bd, int inputLength)
        {
            return (int) Math.Pow(10, inputLength) * ac +
                   (int) Math.Pow(10, inputLength / 2) * (aPlusBTimesCPlusD - ac - bd) +
                   bd;
        }

        private int MakeEvenAndKaratsubaCalc(int number1, int number2)
        {
            var digit1 = number1.ToString().Length;
            var digit2 = number2.ToString().Length;
            var diff = digit1 - digit2;
            var padding = Math.Abs(diff) + 1;
            var adjust = (int) Math.Pow(10, padding);
            return (diff > 0
                ? KaratsubaCalc(number1 * 10, number2 * adjust)
                : KaratsubaCalc(number1 * adjust, number2 * 10)) / adjust / 10;
        }

        public int CalcAAndC(int number1, int number2)
        {
            return KaratsubaCalc(Divide(number1)[0], Divide(number2)[0]);
        }

        public int CalcBAndD(int number1, int number2)
        {
            return KaratsubaCalc(Divide(number1)[1], Divide(number2)[1]);
        }

        public int CalcAPlusBTimesCPlusD(int number1, int number2)
        {
            var div1 = Divide(number1);
            var div2 = Divide(number2);
            return KaratsubaCalc(div1.Sum(), div2.Sum());
        }

        private List<int> Divide(int i)
        {
            var s = i.ToString();
            var firstHalf = s.Substring(0, s.Length / 2);
            var secondHalf = s.Substring(s.Length / 2, s.Length - s.Length / 2);
            return new List<int> {Int32.Parse(firstHalf), Int32.Parse(secondHalf)};
        }

        private List<string> Divide(string i)
        {
            var firstHalf = i.Substring(0, i.Length / 2);
            var secondHalf = i.Substring(i.Length / 2, i.Length - i.Length / 2);
            return new List<string> {firstHalf, secondHalf};
        }

        public string Add(string number1, string number2)
        {
            var result = new Stack<int>();
            var carry = 0;
            while (number1.Any() || number2.Any())
            {
                var add = ConvertCharToInt(number1.LastOrDefault()) + ConvertCharToInt(number2.LastOrDefault()) + carry;
                carry = add / 10;
                result.Push(add % 10);
                number1 = RemoveLastDigit(number1);
                number2 = RemoveLastDigit(number2);
            }

            if (carry > 0)
            {
                result.Push(carry);
            }

            return string.Join("", result);
        }

        public string Subtract(string larger, string smaller)
        {
            var result = new Stack<int>();
            var borrow = 0;
            while (larger.Any() || smaller.Any())
            {
                var lastDigit1 = ConvertCharToInt(larger.LastOrDefault());
                var lastDigit2 = ConvertCharToInt(smaller.LastOrDefault());
                if (lastDigit1 - borrow >= lastDigit2)
                {
                    result.Push(lastDigit1 - lastDigit2 - borrow);
                    borrow = 0;
                }
                else
                {
                    result.Push(lastDigit1 + 10 - lastDigit2 - borrow);
                    borrow = 1;
                }

                larger = RemoveLastDigit(larger);
                smaller = RemoveLastDigit(smaller);
            }

            return RemoveLeadingZerosAndToString(result);
        }

        private int ConvertCharToInt(char c)
        {
            return c == '\u0000' ? 0 : int.Parse(c.ToString());
        }

        private string RemoveLeadingZerosAndToString(Stack<int> result)
        {
            while (result.Any() && result.Peek() == 0)
            {
                result.Pop();
            }

            return !result.Any() ? "0" : string.Join("", result);
        }

        private string RemoveLastDigit(string larger)
        {
            if (larger.Any()) larger = larger.Remove(larger.Length - 1);
            return larger;
        }
    }
}