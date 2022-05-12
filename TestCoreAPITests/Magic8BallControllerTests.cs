using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestCoreAPI.Controllers;


namespace TestCoreAPITests
{
    [TestFixture]
    [Parallelizable]
    public class Magic8BallControllerTests
    {
        private Magic8BallController magic8BallController = new();

        [SetUp]
        public void Setup()
        {
            magic8BallController = new Magic8BallController();
        }

        [Test]
        public void Magic8BallAnswerNotEmpty()
        {
            String result = magic8BallController.Magic8Ball();

            Assert.IsTrue(result.Length > 0, "The answer should not be empty. Expected the answer length to be > 0 but was: " + result.Length);
        }

        
        [Test]
        public void Magic8BallAnswerDifferent()
        {
            int loopCnt = 3;
            List<String> eightBallAnswers = new();

            for (int i = 0; i < loopCnt; i++)
            {
                String result = magic8BallController.Magic8Ball();
                eightBallAnswers.Add(result);
            }

            bool answersAllSame = eightBallAnswers.TrueForAll(x => x.Equals(eightBallAnswers[0]));

            Assert.IsFalse(answersAllSame, "Expected at least one of the " + loopCnt + " answers to be different. Answer were all: '" + eightBallAnswers[0] + "'");
        }

    }
}
