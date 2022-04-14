using NUnit.Framework;
using System;
using TestCoreAPI.MathHelpers;
using System.Collections.Generic;


namespace TestCoreAPITests
{
    [TestFixture]
    public class PrimeFactorsAdditionalTests
    {
        private PrimeFactors primeFactors = new PrimeFactors();

        private static List<TestCaseData> getPrimeFactorsData = new List<TestCaseData>()
        {
            new TestCaseData(1).Returns("1 = 1"),
            new TestCaseData(2).Returns("2 = 2"),
            new TestCaseData(3).Returns("3 = 3"),
            new TestCaseData(4).Returns("2 x 2 = 4"),
            new TestCaseData(5).Returns("5 = 5"),
            new TestCaseData(6).Returns("2 x 3 = 6"),
            new TestCaseData(7).Returns("7 = 7"),
            new TestCaseData(8).Returns("2 x 2 x 2 = 8"),
            new TestCaseData(9).Returns("3 x 3 = 9"),
            new TestCaseData(10).Returns("2 x 5 = 10"),
            new TestCaseData(11).Returns("11 = 11"),
            new TestCaseData(12).Returns("2 x 2 x 3 = 12"),
            new TestCaseData(13).Returns("13 = 13"),
            new TestCaseData(14).Returns("2 x 7 = 14"),
            new TestCaseData(15).Returns("3 x 5 = 15"),
            new TestCaseData(16).Returns("2 x 2 x 2 x 2 = 16"),
            new TestCaseData(17).Returns("17 = 17"),
            new TestCaseData(18).Returns("2 x 3 x 3 = 18"),
            new TestCaseData(19).Returns("19 = 19"),
            new TestCaseData(20).Returns("2 x 2 x 5 = 20"),
            new TestCaseData(21).Returns("3 x 7 = 21"),
            new TestCaseData(32).Returns("2 x 2 x 2 x 2 x 2 = 32"),
            new TestCaseData(33).Returns("3 x 11 = 33"),
            new TestCaseData(63).Returns("3 x 3 x 7 = 63"),
            new TestCaseData(64).Returns("2 x 2 x 2 x 2 x 2 x 2 = 64"),
            new TestCaseData(100).Returns("2 x 2 x 5 x 5 = 100"),
            new TestCaseData(534).Returns("2 x 3 x 89 = 534"),
            new TestCaseData(1077).Returns("3 x 359 = 1077")
        };


        [SetUp]
        public void Setup()
        {
            primeFactors = new PrimeFactors();
        }


        [TestCaseSource(nameof(getPrimeFactorsData))]
        [Parallelizable(ParallelScope.All)]
        public String PrimeFactorsEquationOfNumber(int testNumber)
        {
            String result = primeFactors.GetPrimeFactorsAsEquation(testNumber);
            return result;
        }

    }
}