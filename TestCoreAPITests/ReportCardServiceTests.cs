using NUnit.Framework;
using System;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;

namespace TestCoreAPITests
{
    [TestFixture]
    internal class ReportCardServiceTests
    {
        public ReportCardService reportCardService = new();

        [SetUp]
        public void Setup()
        {
        }

        private static readonly object[] ScoreToGrade_TestCases =
        {
            new TestCaseData(new TestDTO("Don", "French", "FINAL", 100), "A").SetName("A_UpperBound"),
            new TestCaseData(new TestDTO("Becky", "Math", "MID-TERM", 90), "A").SetName("A_LowerBound"),
            new TestCaseData(new TestDTO("Joe", "Algebra", "FINAL", 89), "B").SetName("B_UpperBound"),
            new TestCaseData(new TestDTO("Fred", "English", "MID-TERM", 80), "B").SetName("B_LowerBound"),
            new TestCaseData(new TestDTO("Micky", "Literature", "FINAL", 79), "C").SetName("C_UpperBound"),
            new TestCaseData(new TestDTO("Minnie", "Fantasy", "MID-TERM", 70), "C").SetName("C_LowerBound"),
            new TestCaseData(new TestDTO("Lou", "Science", "FINAL", 69), "D").SetName("D_UpperBound"),
            new TestCaseData(new TestDTO("Leslie", "Geometry", "MID-TERM", 60), "D").SetName("D_LowerBound"),
            new TestCaseData(new TestDTO("Frank", "Biology", "FINAL", 59), "F").SetName("F_UpperBound"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 0), "F").SetName("F_LowerBound"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 101), "U").SetName("ScoreGreaterThan100"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", -1), "U").SetName("ScoreLessThan0")
        };

        [TestCaseSource(nameof(ScoreToGrade_TestCases))]
        public void VerifyTestGrade(TestDTO testScoreInfo, String expectedGrade)
        {
            ReportCardDTO reportCard =reportCardService.GradeTest(testScoreInfo);

            Assert.IsTrue(reportCard.StudentName.CompareTo(testScoreInfo.studentName) == 0, "The studentName should equal: " + testScoreInfo.studentName + " but it was: " + reportCard.StudentName);
            Assert.IsTrue(reportCard.ClassName.CompareTo(testScoreInfo.className) == 0, "The className should equal: " + testScoreInfo.className + " but it was: " + reportCard.ClassName);
            Assert.IsTrue(reportCard.Grade.CompareTo(expectedGrade) == 0, "The grade should equal: " + expectedGrade + ", but it was: " + reportCard.Grade);
        }

    }
}
