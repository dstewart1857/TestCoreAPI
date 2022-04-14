using NUnit.Framework;
using System;
using TestCoreAPI.MathHelpers;
using System.Collections.Generic;


namespace TestCoreAPITests
{
    [TestFixture]
    public class Tests
    {
        private PrimeFactors primeFactors = new PrimeFactors();

        [SetUp]
        public void Setup()
        {
            primeFactors = new PrimeFactors();
        }

        [Test]
        [TestCase(1, "1")]
        [TestCase(2, "2")]
        [TestCase(3, "3")]
        [TestCase(4, "2,2")]
        [Parallelizable(ParallelScope.All)]
        public void PrimeFactorsOfNumber(int testNumber, String expected)
        {
            List<int> resultList = primeFactors.GetPrimeFactors(testNumber);
            String result = String.Join(",", resultList);
            if (result.CompareTo(expected) == 0)
            {
                Console.WriteLine(result);
            }
            Assert.IsTrue(result.CompareTo(expected) == 0, "The result should equal: " + expected + " but it was: " + result);
        }

        [Test]
        [TestCase(1, "1 = 1")]
        [TestCase(2, "2 = 2")]
        [TestCase(3, "3 = 3")]
        [TestCase(4, "2 x 2 = 4")]
        [Parallelizable(ParallelScope.All)]
        public void PrimeFactorsEquationOfNumber(int testNumber, String expected)
        {
            String result = primeFactors.GetPrimeFactorsAsEquation(testNumber);
            if (result.CompareTo(expected) == 0)
            {
                Console.WriteLine(result);
            }
            Assert.IsTrue(result.CompareTo(expected) == 0, "The result should equal: " + expected + " but it was: " + result);
        }

       
        [Test]
        public void PrimesOf1()
        {
            String target = "1 = 1";

            String result = primeFactors.GetPrimeFactorsAsEquation(1);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf2()
        {
            String target = "2 = 2";

            String result = primeFactors.GetPrimeFactorsAsEquation(2);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }
        
        [Test]
        public void PrimesOf3()
        {
            String target = "3 = 3";

            String result = primeFactors.GetPrimeFactorsAsEquation(3);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf4()
        {
            String target = "2 x 2 = 4";

            String result = primeFactors.GetPrimeFactorsAsEquation(4);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf5()
        {
            String target = "5 = 5";

            String result = primeFactors.GetPrimeFactorsAsEquation(5);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf6()
        {
            String target = "2 x 3 = 6";

            String result = primeFactors.GetPrimeFactorsAsEquation(6);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf7()
        {
            String target = "7 = 7";

            String result = primeFactors.GetPrimeFactorsAsEquation(7);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf8()
        {
            String target = "2 x 2 x 2 = 8";

            String result = primeFactors.GetPrimeFactorsAsEquation(8);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf9()
        {
            String target = "3 x 3 = 9";

            String result = primeFactors.GetPrimeFactorsAsEquation(9);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf10()
        {
            String target = "2 x 5 = 10";

            String result = primeFactors.GetPrimeFactorsAsEquation(10);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf11()
        {
            String target = "11 = 11";

            String result = primeFactors.GetPrimeFactorsAsEquation(11);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf12()
        {
            String target = "2 x 2 x 3 = 12";

            String result = primeFactors.GetPrimeFactorsAsEquation(12);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf13()
        {
            String target = "13 = 13";

            String result = primeFactors.GetPrimeFactorsAsEquation(13);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf14()
        {
            String target = "2 x 7 = 14";

            String result = primeFactors.GetPrimeFactorsAsEquation(14);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf15()
        {
            String target = "3 x 5 = 15";

            String result = primeFactors.GetPrimeFactorsAsEquation(15);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf16()
        {
            String target = "2 x 2 x 2 x 2 = 16";

            String result = primeFactors.GetPrimeFactorsAsEquation(16);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }

        [Test]
        public void PrimesOf17()
        {
            String target = "17 = 17";

            String result = primeFactors.GetPrimeFactorsAsEquation(17);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target + " but it was: " + result);
        }
    }
}