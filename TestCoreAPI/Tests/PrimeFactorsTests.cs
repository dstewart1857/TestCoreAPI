using NUnit.Framework;
using TestCoreAPI.MathHelpers;

namespace TestCoreAPI.Tests
{
    [TestFixture]   
    public class PrimeFactorsTests
    {
        private PrimeFactors primeFactors;

        [SetUp]
        public void SetUp()
        {
            primeFactors = new PrimeFactors();
        }

        [Test]
        public void primesOf1()
        {
            String target = "1 = 1";

            String result = primeFactors.getPrimeFactorsAsEquation(1);

            Assert.IsTrue(result.CompareTo(target) == 0, "The result should equal: " + target);
        }
    }
}
