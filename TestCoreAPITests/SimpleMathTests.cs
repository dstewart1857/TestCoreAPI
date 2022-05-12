using NUnit.Framework;
using System;
using TestCoreAPI.Controllers;


namespace TestCoreAPITests
{
    [TestFixture]
    [Parallelizable]
    public class SimpleMathControllerTests
    {
        private SimpleMathController simpleMathController = new();

        [SetUp]
        public void Setup()
        {
            simpleMathController = new SimpleMathController();
        }

        [Test]
        public void SimpleMathNullOperation()
        {
            String expectedValue = "5.50";

            String result = simpleMathController.simpleMath(2.25F, 3.25F, '\0').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathUnknownOperation()
        {
            String expectedValue = "2.75";

            String result = simpleMathController.simpleMath(1.5F, 1.25F, 'z').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathSubtractLowerCase()
        {
            String expectedValue = "0.75";

            String result = simpleMathController.simpleMath(1.5F, 0.75F, 's').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathSubtractUpperCase()
        {
            String expectedValue = "-0.75";

            String result = simpleMathController.simpleMath(0.75F, 1.5F, 'S').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathMultiplyLowerCase()
        {
            String expectedValue = "4.50";

            String result = simpleMathController.simpleMath(1.5F, 3.0F, 'm').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathMultiplyUpperCase()
        {
            String expectedValue = "-4.50";

            String result = simpleMathController.simpleMath(1.5F, -3.0F, 'M').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathRemainderLowerCase()
        {
            String expectedValue = "0.50";

            String result = simpleMathController.simpleMath(4.5F, 2.0F, 'r').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test]
        public void SimpleMathRemainderUpperCase()
        {
            String expectedValue = "0.50";

            String result = simpleMathController.simpleMath(4.5F, 2.0F, 'R').ToString("n2");

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }

        [Test(Description="Tests math remainder operation and displays equation & answer on the console.")]
        public void SimpleMathRemainderWithConsoleLog()
        {
            String expectedValue = "0.50";
            float operandOne = 4.5F;
            float operandTwo = 2.0F;

            String result = simpleMathController.simpleMath(operandOne, operandTwo, 'R').ToString("n2");

            if (result.CompareTo(expectedValue) == 0)
            {
                Console.WriteLine(operandOne.ToString() + " % " + operandTwo + " = " + result);
            }

            Assert.IsTrue(result.CompareTo(expectedValue) == 0, "The result should equal: " + expectedValue + " but it was: " + result);
        }
    }
}
