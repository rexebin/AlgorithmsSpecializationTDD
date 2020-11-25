using System;
using NUnit.Framework;

namespace DivideAndConquerTDD.Karatsuba
{
    public class KaratsubaTest
    {
        private Karatsuba sut = null!;

        [SetUp]
        public void Setup()
        {
            sut = new Karatsuba();
        }


        [Test]
        [TestCase(12, 34, 3)]
        public void GivenDoubleDigits_ShouldReturnAC(long number1, long number2, long expected)
        {
            Assert.AreEqual(expected, sut.CalcAAndC(number1, number2));
        }

        [Test]
        [TestCase(12, 34, 8)]
        public void GivenDoubleDigits_ShouldReturnBD(long number1, long number2, long expected)
        {
            Assert.AreEqual(expected, sut.CalcBAndD(number1, number2));
        }

        [Test]
        [TestCase(12, 34, 21)]
        [TestCase(1234, 5678, 6164)]
        public void GivenTwoDoubleDigits_ShouldReturnAPlusBTimesCPLusD(long number1, long number2, long expected)
        {
            Assert.AreEqual(expected, sut.CalcAPlusBTimesCPlusD(number1, number2));
        }

        [Test]
        [TestCase(12, 34, 408)]
        [TestCase(1234, 5678, 7006652)]
        [TestCase(12, 123, 1476)]
        [TestCase(1234, 12345, 15233730)]
        [TestCase(123, 1234, 151782)]
        public void GivenTwoNumbers_CalculateProductWithKaratsubaAlgorithm(long number1, long number2, long expected)
        {
            Assert.AreEqual(expected, sut.KaratsubaCalc(number1, number2));
        }
        

        [Test]
        [TestCase("1", "2", "3")]
        [TestCase("12", "23", "35")]
        [TestCase("99", "99", "198")]
        [TestCase("12399", "99", "12498")]
        public void GivenStringNumbers_ShouldCalculateAdditionAndReturnResultInString(string number1, string number2,
            string expected)
        {
            Assert.AreEqual(expected, sut.Add(number1, number2));
        }
        
        [Test]
        [TestCase("2", "1", "1")]
        [TestCase("23", "12", "11")]
        [TestCase("99", "99", "0")]
        [TestCase("12399", "99", "12300")]
        [TestCase("12399", "4899", "7500")]
        [TestCase("61640", "672", "60968")]
        
        public void GivenStringNumbers_ShouldCalculateSubtractAndReturnResultInString(string number1, string number2,
            string expected)
        {
            Assert.AreEqual(expected, sut.Subtract(number1, number2));
        }
        [Test]
        [TestCase("12", "34", "408")]
        [TestCase("12", "123", "1476")]
        [TestCase("1234", "5678", "7006652")]
        [TestCase("1234", "12345", "15233730")]
        [TestCase("123", "1234", "151782")]
        [TestCase("1234567", "5678567", "7010571425489")]
        [TestCase("3141592653589793238462643383279502884197169399375105820974944592", 
            "2718281828459045235360287471352662497757247093699959574966967627", 
            "8539734222673567065463550869546574495034888535765114961879601127067743044893204848617875072216249073013374895871952806582723184")]
        public void GivenTwoNumbersAsString_CalculateProductWithKaratsubaAlgorithmAndReturnString(string number1, string number2, string expected)
        {
            Assert.AreEqual(expected, sut.KaratsubaCalc(number1, number2));
            sut.PrintKaratTimes();
        }
      
    }
}