using NUnit.Framework;
using System;
using TestCoreAPI.MathHelpers;

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
        public void PrimesOf1()
        {
            String target = "1 = 1";

            String result = primeFactors.GetPrimeFactorsAsEquation(1);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target);
        }
    }
}