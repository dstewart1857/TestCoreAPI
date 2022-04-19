using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;

namespace TestCoreAPITests
{
    [TestFixture]
    internal class ReportCardServiceTests
    {
        public List<TestDTO> testDTOs = new List<TestDTO>();
        public ReportCardService reportCardService = new ReportCardService();

        [SetUp]
        public void Setup()
        {
            initializeTestDTOs(testDTOs);
        }

        private void initializeTestDTOs(List<TestDTO> testDTOs)
        {
            testDTOs.Add(getTestDTO("English 2010", "Bucky Goldstein", "Shakespeare's 50K Word Vocabulary", 88));
            testDTOs.Add(getTestDTO("English 2010", "Zeke T. Prescott, III", "Shakespeare's 50K Word Vocabulary", 61));
            testDTOs.Add(getTestDTO("English 2010", "Sally Jessie Raphael", "Shakespeare's 50K Word Vocabulary", 74));
            testDTOs.Add(getTestDTO("English 2010", "JoJo McFarland", "Shakespeare's 50K Word Vocabulary", 31));
            testDTOs.Add(getTestDTO("Math 3010", "Bucky Goldstein", "4D Algorithims", 98));
            testDTOs.Add(getTestDTO("Math 3010", "Zeke T. Prescott, III", "4D Algorithims", 67));
            testDTOs.Add(getTestDTO("Math 3010", "Wicket W. Warkick", "4D Algorithims", 84));
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
        public void SortReportCardsByGrade()
        {
            List<ReportCardDTO> reportCardDTOs = reportCardService.sortGrades(testDTOs, 1);
            Assert.IsTrue(reportCardDTOs.ElementAt(0).grade.CompareTo("A") == 0, "Grade should equal 'B', but was: " + reportCardDTOs.ElementAt(0).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(1).grade.CompareTo("B") == 0, "Grade should equal 'B', but was: " + reportCardDTOs.ElementAt(1).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(2).grade.CompareTo("B") == 0, "Grade should equal 'C', but was: " + reportCardDTOs.ElementAt(2).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(3).grade.CompareTo("C") == 0, "Grade should equal 'D', but was: " + reportCardDTOs.ElementAt(3).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(4).grade.CompareTo("D") == 0, "Grade should equal 'F', but was: " + reportCardDTOs.ElementAt(4).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(5).grade.CompareTo("D") == 0, "Grade should equal 'B', but was: " + reportCardDTOs.ElementAt(5).grade);
            Assert.IsTrue(reportCardDTOs.ElementAt(6).grade.CompareTo("F") == 0, "Grade should equal 'B', but was: " + reportCardDTOs.ElementAt(6).grade);
        }

        [Test]
        public void Test_GetAverageMetricsByClass()
        {
            List<ClassAveragesDTO> metrics = reportCardService.getAverageMetricsByClass(testDTOs);
            Assert.IsNotNull(metrics);
            Assert.IsTrue(metrics.ElementAt(0).className.CompareTo("English 2010") == 0, "Value at index 0 should be: English 2010, but is: " + metrics.ElementAt(0).className);
            Assert.IsTrue(metrics.ElementAt(0).averageGrade.CompareTo("D") == 0, "Value at index 0 should be: 'D', but is: " + metrics.ElementAt(0).averageGrade);
            Assert.IsTrue(metrics.ElementAt(0).averageScore == 63.5, "Value at index 0 should be: 63.5, but is: " + metrics.ElementAt(0).averageScore);
            Assert.IsTrue(metrics.ElementAt(1).className.CompareTo("Math 3010") == 0, "Value at index 1 should be: English 2010, but is: " + metrics.ElementAt(1).className);
            Assert.IsTrue(metrics.ElementAt(1).averageGrade.CompareTo("B") == 0, "Value at index 0 should be: 'B', but is: " + metrics.ElementAt(1).averageGrade);
            Assert.IsTrue(metrics.ElementAt(1).averageScore == 83, "Value at index 0 should be: 83, but is: " + metrics.ElementAt(1).averageScore);


        }
    }
}
