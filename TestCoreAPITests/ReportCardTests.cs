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
        public TestDTO testDTO = new TestDTO();
        public ReportCardService reportCardService = new ReportCardService();

    [SetUp]
        public void Setup()
        {
            testDTO.studentName = "Bucky Goldstein";
            testDTO.className = "English 2010";
            testDTO.testName = "Shakesphere and You";
            testDTO.score = 95;
        }

        [Test]
        public void Test_GradeTest()
        {
            ReportCardDTO reportCard =reportCardService.GradeTest(testDTO);

            Assert.IsTrue(reportCard.studentName.CompareTo(testDTO.studentName) == 0, "The studentName should equal: " + testDTO.studentName + " but it was: " + reportCard.studentName);
            Assert.IsTrue(reportCard.className.CompareTo(testDTO.className) == 0, "The className should equal: " + testDTO.className + " but it was: " + reportCard.className);
            Assert.IsTrue(reportCard.grade.CompareTo("A") == 0, "The grade should equal: A, but it was: " + reportCard.grade);
            
        }
    }
}
