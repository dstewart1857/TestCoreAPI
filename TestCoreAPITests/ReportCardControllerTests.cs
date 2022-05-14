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
        public List<TestDTO> largeSingleTestDTOs = new();
        public List<TestDTO> largeMultiTestDTOs = new();
        public ReportCardController reportCardController = new();
        private float expectedAvgScoreLargeMulti, expectedAvgScoreLargeSingle;

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            InitializeTestDTOs();
            Console.WriteLine("OneTimeSetup: Test collection was initialized with " + testDTOs.Count + " test objects.");
        }

        [SetUp]
        public void Setup()
        {
            var currentTestName = TestContext.CurrentContext.Test.Name;
            Console.WriteLine("Executing setup for " + currentTestName);
            switch(currentTestName)
            {
                case "GetAvgMetricsByClass_LargeMultiTestCollection":
                {
                    reportCardController.SubmitTests(largeMultiTestDTOs);
                    break;
                }
                case "GetAvgMetricsByClass_LargeSingleTestCollection":
                {
                    reportCardController.SubmitTests(largeSingleTestDTOs);
                    break;
                }
            }    
        }

        private void InitializeTestDTOs()
        {
            testDTOs.Add(new TestDTO("Bucky Goldstein", "English 2010", "Shakesphere's 50K Word Vocabulary", 88));
            testDTOs.Add(new TestDTO("Zeke T. Prescott III", "English 2010", "Shakesphere's 50K Word Vocabulary", 61));
            testDTOs.Add(new TestDTO("Sally Jessie Raphael", "English 2010", "Shakesphere's 50K Word Vocabulary", 74));
            testDTOs.Add(new TestDTO("JoJo McFarland", "English 2010", "Shakesphere's 50K Word Vocabulary", 31));
            testDTOs.Add(new TestDTO("Fred Flintstone", "English 2010", "Shakesphere's 50K Word Vocabulary", 85));
            testDTOs.Add(new TestDTO("Barney Rubble", "English 2010", "Shakesphere's 50K Word Vocabulary", 101));
            testDTOs.Add(new TestDTO("Bucky Goldstein", "Math 3010", "4D Algorithims", 92));
            testDTOs.Add(new TestDTO("Agustus Jones", "Math 3010", "4D Algorithims", 87));
            testDTOs.Add(new TestDTO("Scooter McDoolaby", "Math 3010", "4D Algorithims", 61));

            // Test DTOs for large class single test collection set
            int classSizeSingle = 100;
            int[] testScoresSingle = {73, 85};
            expectedAvgScoreLargeSingle = (float)(testScoresSingle[0] + testScoresSingle[1]) / (float)testScoresSingle.Length;

            for (int i = 1; i <= classSizeSingle; i++)
            {
                int x = i % 2;
                largeSingleTestDTOs.Add(new TestDTO("Student_LargeSingle_" + i, "Class_LargeSingleTest", "TestName_Large_Single", testScoresSingle[x]));
            }

            // Test DTOs for large class multi-test collection set
            int classSize = 50;
            int[] testScores = {75, 85, 95, 80};
            expectedAvgScoreLargeMulti = (float)(testScores[0]+testScores[1]+testScores[2]+testScores[3]) / (float)testScores.Length;

            for(int i = 1; i <= classSize; i++)
            {
                largeMultiTestDTOs.Add(new TestDTO("Student_Large_" + i, "Class_LargeMulti-Test", "TestName_Large_1", testScores[0]));
                largeMultiTestDTOs.Add(new TestDTO("Student_Large_" + i, "Class_LargeMulti-Test", "TestName_Large_2", testScores[1]));
                largeMultiTestDTOs.Add(new TestDTO("Student_Large_" + i, "Class_LargeMulti-Test", "TestName_Large_3", testScores[2]));
                largeMultiTestDTOs.Add(new TestDTO("Student_Large_" + i, "Class_LargeMulti-Test", "TestName_Large_4", testScores[3]));
            }
        }

        [Test, Order(10)]
        public void GetTestsListInitial()
        {
            int testCollectionSize = reportCardController.GetTestCollection().Count;
            Console.WriteLine("Initial test collection size (before adding a test collection): " + testCollectionSize);

            Assert.IsTrue(testCollectionSize.CompareTo(0) == 0, "The initial collection size should be 0 but it was: " + testCollectionSize);
        }

        [Test, Order(20)]
        public void AddTestsToList()
        {
            int testCollectionSizeBegin = reportCardController.GetTestCollection().Count;
            Console.WriteLine("Initial test collection size: " + testCollectionSizeBegin);
            reportCardController.SubmitTests(testDTOs);
            int testCollectionSizeEnd = reportCardController.GetTestCollection().Count;
            Console.WriteLine("After adding a test collection the size is: " + testCollectionSizeEnd);

            Assert.IsTrue(testCollectionSizeBegin.CompareTo(testCollectionSizeEnd) < 0, "The collection size should be greater than the initial size. Expected: " + (testCollectionSizeBegin+testDTOs.Count) + " but it was: " + testCollectionSizeEnd);
        }

        [Test, Order(30)]
        public void GetTestsList()
        {
            List<TestDTO> testCollection = reportCardController.GetTestCollection();
            int testCollectionSize = testCollection.Count;

            Assert.IsTrue(testCollectionSize.CompareTo(0) > 0, "The collection size should be greater than 0 but was: " + testCollectionSize);

            if (testCollectionSize > 0)
            {
                Console.WriteLine("Test Collection:");
                foreach (TestDTO testDTO in testCollection)
                {
                    Console.WriteLine("  Class Name: " + testDTO.className + ", Student: " + testDTO.studentName + ", Test Name: " + testDTO.testName + ", Score: " + testDTO.score);
                }
            }

            Assert.IsTrue(testCollectionSize == testDTOs.Count, "The collection size should be: " + testDTOs.Count + " But was: " + testCollectionSize);

            // Verify contents of test list
            bool testExists = testCollection.Exists(x => x.studentName == testDTOs[0].studentName);
            Assert.IsTrue(testExists, "The test collection list does NOT contain a test with the student name of: " + testDTOs[0].studentName);
        }

        [Test, Order(40)]
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

            var objectResult = (ObjectResult?)response.Result;
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


        [Test, Order(11)]
        public void GetAvgMetricsByClass_EmptyCollection()
        {
            int listSize = reportCardController.GetAverageMetricsByClass().Count;

            //Console.WriteLine("After adding a test collection the size is: " + testCollectionSizeEnd);
            Assert.IsTrue(listSize == 0, "The size of the average metrics list should be 0 for an empty test collection. Received a size of: " + listSize);
        }

        [Test, Order(21)]
        public void GetAvgMetricsByClass_InitialCollection()
        {
            int listSize = reportCardController.GetAverageMetricsByClass().Count;

            //Console.WriteLine("After adding a test collection the size is: " + testCollectionSizeEnd);
            Assert.IsTrue(listSize == 2, "The size of the average metrics list should be 2 for the initial test collection. Received a size of: " + listSize);
        }

        [Test, Order(50)]
        public void GetAvgMetricsByClass_LargeSingleTestCollection()
        {
            List<ClassAveragesDTO> classAveragesList = reportCardController.GetAverageMetricsByClass();
            int listSize = classAveragesList.Count;

            //Console.WriteLine("After adding a large single test collection the size is: " + testCollectionSizeEnd);
            Assert.IsTrue(listSize == 3, "The size of the average metrics list should be 3 for the large single test collection. Received a size of: " + listSize);

            ClassAveragesDTO? largeClass = classAveragesList.Find(x => x.ClassName == "Class_LargeSingleTest");

            Assert.IsNotNull(largeClass, "A class averages result should have been returned for the Class_LargeSingleTest class. Actual result: Nothing was returned.");
            if (largeClass != null)
            {
                Assert.IsTrue(largeClass.AverageScore == expectedAvgScoreLargeSingle, "Expected the average score for the large single test class to be " + expectedAvgScoreLargeSingle + ". Actual result: " + largeClass.AverageScore);
                Assert.IsTrue(largeClass.AverageGrade == "C", "Expected the average grade for the large single test class to be 'C'. Actual result: '" + largeClass.AverageGrade + "'");
            }
        }


        [Test, Order(60)]
        public void GetAvgMetricsByClass_LargeMultiTestCollection()
        {
            List<ClassAveragesDTO> classAveragesList = reportCardController.GetAverageMetricsByClass();
            int listSize = classAveragesList.Count;

            //Console.WriteLine("After adding a large multi-test collection the size is: " + testCollectionSizeEnd);
            Assert.IsTrue(listSize == 4, "The size of the average metrics list should be 4 for the large multi-test collection. Received a size of: " + listSize);

            ClassAveragesDTO? largeClass = classAveragesList.Find(x => x.ClassName == "Class_LargeMulti-Test");

            Assert.IsNotNull(largeClass, "A class averages result should have been returned for the Class_LargeMulti-Test class. Actual result: Nothing was returned.");
            if (largeClass != null)
            {
                Assert.IsTrue(largeClass.AverageScore == expectedAvgScoreLargeMulti, "Expected the average score for the large multi-test class to be " + expectedAvgScoreLargeMulti + ". Actual result: " + largeClass.AverageScore);
                Assert.IsTrue(largeClass.AverageGrade == "B", "Expected the average grade for the large multi-test class to be 'B'. Actual result: '" + largeClass.AverageGrade + "'");
            }
        }

        [Test, Order(70)]
        public void GetCandlestickChartData_TooFewTests()
        {
            var response = reportCardController.GetCandlestickChartData();

            var objectResult = (ObjectResult?)response.Result;
            if (objectResult != null)
            {
                if(objectResult.StatusCode < 400)
                {
                    Assert.Fail("Expected an HTTP status code >= 400 but was " + objectResult.StatusCode + " instead.");
                }
            }
            else
            {
                Assert.Fail("The response result was null when it was NOT expected to be.");
            }
        }

        [Test, Order(80)]
        public void GetCandlestickChartData_ValidData()
        {
            List<TestDTO> testDTOList = new();
            testDTOList.Add(new TestDTO("Candlestick Check", "Math 3010", "4D Algorithims", 81));
            reportCardController.SubmitTests(testDTOList);

            var response = reportCardController.GetCandlestickChartData();

            var objectResult = (ObjectResult?)response.Result;
            if (objectResult != null)
            {
                if (objectResult.StatusCode != 200)
                {
                    Console.WriteLine(objectResult.Value);
                    Assert.Fail("Expected an HTTP status code of 200 but was " + objectResult.StatusCode + " instead.");
                }

                var CandlestickDTOs = (List<CandlestickDTO>?)objectResult.Value;
                if (CandlestickDTOs != null)
                {
                    Assert.IsTrue(CandlestickDTOs.Count == 4, "Expected the number of candlestick data objects to be: 4  Actual number received was: " + CandlestickDTOs.Count);
                }
            }
            else
            {
                Assert.Fail("The response result was null when it was NOT expected to be.");
            }
        }


    }
}
