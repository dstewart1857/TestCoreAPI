using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestCoreAPI.Controllers;
using TestCoreAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TestCoreAPITests
{
    [TestFixture]
    internal class ReportCardControllerTests
    {
        public List<TestDTO> testDTOs = new();
        public ReportCardController reportCardController = new();

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            InitializeTestDTOs();
            Console.WriteLine("OneTimeSetup: Test collection was initialized with " + testDTOs.Count + " test objects.");
        }

        [SetUp]
        public void Setup()
        {
            //initializeTestDTOs();
        }

        private void InitializeTestDTOs()
        {
            testDTOs.Add(new TestDTO("Bucky Goldstein", "English 2010", "Shakesphere's 50K Word Vocabulary", 88));
            testDTOs.Add(new TestDTO("Zeke T. Prescott III", "English 2010", "Shakesphere's 50K Word Vocabulary", 61));
            testDTOs.Add(new TestDTO("Sally Jessie Raphael", "English 2010", "Shakesphere's 50K Word Vocabulary", 74));
            testDTOs.Add(new TestDTO("JoJo McFarland", "English 2010", "Shakesphere's 50K Word Vocabulary", 31));
            testDTOs.Add(new TestDTO("Fred Flintstone", "English 2010", "Shakesphere's 50K Word Vocabulary", 85));
            testDTOs.Add(new TestDTO("Barney Rubble", "English 2010", "Shakesphere's 50K Word Vocabulary", 101));
        }

        [Test, Order(1)]
        public void GetTestsListInitial()
        {
            int testCollectionSize = reportCardController.GetTestCollection().Count;
            Console.WriteLine("Initial test collection size (before adding a test collection): " + testCollectionSize);

            Assert.IsTrue(testCollectionSize.CompareTo(0) == 0, "The initial collection size should be 0 but it was: " + testCollectionSize);
        }

        [Test, Order(2)]
        public void AddTestsToList()
        {
            int testCollectionSizeBegin = reportCardController.GetTestCollection().Count;
            Console.WriteLine("Initial test collection size: " + testCollectionSizeBegin);
            reportCardController.SubmitTests(testDTOs);
            int testCollectionSizeEnd = reportCardController.GetTestCollection().Count;
            Console.WriteLine("After adding a test collection the size is: " + testCollectionSizeEnd);

            Assert.IsTrue(testCollectionSizeBegin.CompareTo(testCollectionSizeEnd) < 0, "The collection size should be greater than the initial size. Expected: " + (testCollectionSizeBegin+testDTOs.Count) + " but it was: " + testCollectionSizeEnd);
        }

        [Test, Order(3)]
        public void GetTestsList()
        {
            List<TestDTO> testCollection = reportCardController.GetTestCollection();
            int testCollectionSize = reportCardController.GetTestCollection().Count;

            Assert.IsTrue(testCollectionSize.CompareTo(0) > 0, "The collection size should be greater than 0 but was: " + testCollectionSize);

            if (testCollectionSize > 0)
            {
                Console.WriteLine("Test Collection:");
                foreach (TestDTO testDTO in testCollection)
                {
                    Console.WriteLine("  Class Name: " + testDTO.className + ", Student: " + testDTO.studentName + ", Test Name: " + testDTO.testName + ", Score: " + testDTO.score);
                }
            }

            Assert.IsTrue(testCollectionSize.CompareTo(testDTOs.Count) == 0, "The collection size should be: " + testDTOs.Count + " But was: " + testCollectionSize);

            // Verify contents of test list
            bool testExists = testCollection.Exists(x => x.studentName == testDTOs[0].studentName);
            Assert.IsTrue(testExists, "The test collection list does NOT contain a test with the student name of: " + testDTOs[0].studentName);
        }

        [Test, Order(4)]
        [TestCase(0, TestName="SortListByStudentName")]
        [TestCase(1, TestName="SortListByGrade")]
        public void SortList(int sortMode)
        {
            List<TestDTO> testCollection = reportCardController.GetTestCollection();
            int testCollectionSize = reportCardController.GetTestCollection().Count;

            Assert.IsTrue(testCollectionSize.CompareTo(0) > 0, "The collection size should be greater than 0 but was: " + testCollectionSize);

            if (testCollectionSize > 0)
            {
                Console.WriteLine("Test Collection (before sort):");
                foreach (TestDTO testDTO in testCollection)
                {
                    Console.WriteLine("  Class Name: " + testDTO.className + ", Student: " + testDTO.studentName + ", Test Name: " + testDTO.testName + ", Score: " + testDTO.score);
                }
            }

            var response = reportCardController.SortedGrades(sortMode);

            Assert.NotNull(response, "The response to get sorted grades was null.");
            Assert.IsInstanceOf<ActionResult<List<ReportCardDTO>>>(response, "The response to get sorted grades was not an instance of ActionResult<List<ReportCardDTO>>");
            Assert.IsInstanceOf<ObjectResult>(response.Result, "The response to get sorted grades was not an instance of ObjectResult");

            var objectResult = (OkObjectResult?)response.Result;
            if (objectResult != null)
            {
                Assert.IsTrue(objectResult.StatusCode == 200);
                var reportCardDTOs = (List<ReportCardDTO>?)objectResult.Value;
                if (reportCardDTOs != null)
                {
                    switch(sortMode)
                    {
                        case 0:
                            String studentName = reportCardDTOs[0].StudentName;
                            Console.WriteLine("\nTest Report Cards (sorted by student name):");
                            foreach (ReportCardDTO reportCardDTO in reportCardDTOs)
                            {
                                Assert.IsTrue(reportCardDTO.StudentName.CompareTo(studentName) > 0 || reportCardDTO.StudentName.CompareTo(studentName) == 0, "The current student name was expected to be equal to or less than the sort order of the previous student name. Previous student name: " + studentName + " Current student name: " + reportCardDTO.StudentName);
                                Console.WriteLine("  Class Name: " + reportCardDTO.ClassName + ", Student: " + reportCardDTO.StudentName + ", Grade: " + reportCardDTO.Grade);
                                studentName = reportCardDTO.StudentName;
                            }
                            break;
                        case 1:
                            String grade = reportCardDTOs[0].Grade;
                            Console.WriteLine("\nTest Report Cards (sorted by grade):");
                            foreach (ReportCardDTO reportCardDTO in reportCardDTOs)
                            {
                                Assert.IsTrue(reportCardDTO.Grade.CompareTo(grade) > 0 || reportCardDTO.Grade.CompareTo(grade) == 0, "The current grade was expected to be equal to or less than the previous grade. Previous grade: " + grade + " Current grade: " + reportCardDTO.Grade);
                                Console.WriteLine("  Class Name: " + reportCardDTO.ClassName + ", Student: " + reportCardDTO.StudentName + ", Grade: " + reportCardDTO.Grade);
                                grade = reportCardDTO.Grade;
                            }
                            break;
                        default:
                            Assert.Fail("The provided sort mode is not supported! Provided mode: " + sortMode);
                            break;
                    }
                }
                else
                {
                    Assert.Fail("The ObjectResult did not have any ReportCardDTO objects!");
                }
            }
            else
            {
                Assert.Fail("The response did not contain an ObjectResult!");
            }
        }

    }
}
