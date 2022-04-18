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
    internal class ReportCardTests
    {
        //public static TestDTO testDTO = new TestDTO();
        public ReportCardService reportCardService = new ReportCardService();

        [SetUp]
        public void Setup()
        {
            //testDTO.studentName = "Bucky Goldstein";
            //testDTO.className = "English 2010";
            //testDTO.testName = "Shakesphere and You";
            //testDTO.score = 95;
        }

        private static IEnumerable<TestCaseData> ScoreToGrade_TestCases()
        {
            yield return new TestCaseData(new TestDTO("Don", "French", "FINAL", 100), "A").SetName("A_UpperBound");
            yield return new TestCaseData(new TestDTO("Becky", "Math", "MID-TERM", 90), "A").SetName("A_LowerBound");
            yield return new TestCaseData(new TestDTO("Joe", "Algebra", "FINAL", 89), "B").SetName("B_UpperBound");
            yield return new TestCaseData(new TestDTO("Fred", "English", "MID-TERM", 80), "B").SetName("B_LowerBound");
            yield return new TestCaseData(new TestDTO("Micky", "Literature", "FINAL", 79), "C").SetName("C_UpperBound");
            yield return new TestCaseData(new TestDTO("Minnie", "Fantasy", "MID-TERM", 70), "C").SetName("C_LowerBound");
            yield return new TestCaseData(new TestDTO("Lou", "Science", "FINAL", 69), "D").SetName("D_UpperBound");
            yield return new TestCaseData(new TestDTO("Leslie", "Geometry", "MID-TERM", 60), "D").SetName("D_LowerBound");
            yield return new TestCaseData(new TestDTO("Frank", "Biology", "FINAL", 59), "F").SetName("F_UpperBound");
            yield return new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 0), "F").SetName("F_LowerBound");
            yield return new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 101), "U").SetName("ScoreGreaterThan100");
            yield return new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", -1), "U").SetName("ScoreLessThan0");
        }


        [TestCaseSource("ScoreToGrade_TestCases")]
        public void GradeTest(TestDTO testScoreInfo, String expectedGrade)
        {
            ReportCardDTO reportCard =reportCardService.GradeTest(testScoreInfo);

            Assert.IsTrue(reportCard.studentName.CompareTo(testScoreInfo.studentName) == 0, "The studentName should equal: " + testScoreInfo.studentName + " but it was: " + reportCard.studentName);
            Assert.IsTrue(reportCard.className.CompareTo(testScoreInfo.className) == 0, "The className should equal: " + testScoreInfo.className + " but it was: " + reportCard.className);
            Assert.IsTrue(reportCard.grade.CompareTo(expectedGrade) == 0, "The grade should equal: " + expectedGrade + ", but it was: " + reportCard.grade);
        }

    }
}
