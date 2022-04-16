using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreAPI.Controllers;
using TestCoreAPI.DTO;

namespace TestCoreAPITests
{
    [TestFixture]
    internal class ReportCardControllerTests
    {
        public List<TestDTO> testDTOs = new List<TestDTO>();
        public ReportCardController reportCardController = new ReportCardController();

        [SetUp]
        public void Setup()
        {
            initializeTestDTOs(testDTOs);
        }

        private void initializeTestDTOs(List<TestDTO> testDTOs)
        {
            testDTOs.Add(getTestDTO("English 2010", "Bucky Goldstein", "Shakesphere's 50K Word Vocabulary", 88));
            testDTOs.Add(getTestDTO("English 2010", "Zeke T. Prescott, III", "Shakesphere's 50K Word Vocabulary", 61));
            testDTOs.Add(getTestDTO("English 2010", "Sally Jessie Raphael", "Shakesphere's 50K Word Vocabulary", 74));
            testDTOs.Add(getTestDTO("English 2010", "JoJo McFarland", "Shakesphere's 50K Word Vocabulary", 31));
        }

        private TestDTO getTestDTO(String className, String studentName, String testName, int score)
        {
            TestDTO testDTO = new TestDTO();
            testDTO.className = className;
            testDTO.studentName = studentName;
            testDTO.testName = testName;
            testDTO.score = score;
            return testDTO;
        }

        [Test]
        public void Test_GetTests()
        {
            List<TestDTO> testCollection = reportCardController.getTestCollection();
            Assert.IsTrue(testCollection != null);
            Assert.IsTrue(testCollection.Count == 0);
            reportCardController.submitTests(testDTOs);
            testCollection = reportCardController.getTestCollection();
            Assert.IsTrue(testCollection.Count == 4);
        }
    }
}
