using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;

namespace TestCoreAPITests
{
    [TestFixture]
    [Parallelizable]
    internal class ReportCardServiceTests
    {
        public ReportCardService reportCardService = new();

        [SetUp]
        public void Setup()
        {
        }

        private List<String> GetCsvTestScores(List<TestDTO> testCollection)
        {
            List<String> testScoresList = new();
            List<string> classNames = new();

            // Get list of class names in testCollection
            foreach (TestDTO testDTO in testCollection)
            {
                if (!classNames.Contains(testDTO.className))
                {
                    classNames.Add(testDTO.className);
                }
            }

            // Get scores for each class and calculate quartiles
            foreach (string className in classNames)
            {
                // Get test scores for each class
                List<int> classTestScores = new();
                foreach (TestDTO testDTO in testCollection)
                {
                    if (testDTO.className == className)
                    {
                        classTestScores.Add(testDTO.score);
                    }
                }

                classTestScores.Sort();
                String classTestScoresString = String.Join(",", classTestScores.ToArray());
                testScoresList.Add(classTestScoresString);
            }

            return testScoresList;
        }

        private static readonly object[] ScoreToGrade_TestCases =
        {
            new TestCaseData(new TestDTO("Don", "French", "FINAL", 100), "A").SetName("ScoreToGrade_A_UpperBound"),
            new TestCaseData(new TestDTO("Becky", "Math", "MID-TERM", 90), "A").SetName("ScoreToGrade_A_LowerBound"),
            new TestCaseData(new TestDTO("Joe", "Algebra", "FINAL", 89), "B").SetName("ScoreToGrade_B_UpperBound"),
            new TestCaseData(new TestDTO("Fred", "English", "MID-TERM", 80), "B").SetName("ScoreToGrade_B_LowerBound"),
            new TestCaseData(new TestDTO("Micky", "Literature", "FINAL", 79), "C").SetName("ScoreToGrade_C_UpperBound"),
            new TestCaseData(new TestDTO("Minnie", "Fantasy", "MID-TERM", 70), "C").SetName("ScoreToGrade_C_LowerBound"),
            new TestCaseData(new TestDTO("Lou", "Science", "FINAL", 69), "D").SetName("ScoreToGrade_D_UpperBound"),
            new TestCaseData(new TestDTO("Leslie", "Geometry", "MID-TERM", 60), "D").SetName("ScoreToGrade_D_LowerBound"),
            new TestCaseData(new TestDTO("Frank", "Biology", "FINAL", 59), "F").SetName("ScoreToGrade_F_UpperBound"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 0), "F").SetName("ScoreToGrade_F_LowerBound"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", 101), "U").SetName("ScoreToGrade_ScoreGreaterThan100"),
            new TestCaseData(new TestDTO("George", "Physics", "MID-TERM", -1), "U").SetName("ScoreToGrade_ScoreLessThan0")
        };

        [TestCaseSource(nameof(ScoreToGrade_TestCases))]
        public void VerifyGradeTest(TestDTO testScoreInfo, String expectedGrade)
        {
            ReportCardDTO reportCard =reportCardService.GradeTest(testScoreInfo);

            Assert.IsTrue(reportCard.StudentName.CompareTo(testScoreInfo.studentName) == 0, "The studentName should equal: " + testScoreInfo.studentName + " but it was: " + reportCard.StudentName);
            Assert.IsTrue(reportCard.ClassName.CompareTo(testScoreInfo.className) == 0, "The className should equal: " + testScoreInfo.className + " but it was: " + reportCard.ClassName);
            Assert.IsTrue(reportCard.Grade.CompareTo(expectedGrade) == 0, "The grade should equal: " + expectedGrade + ", but it was: " + reportCard.Grade);
        }

        [Test]
        public void GetCandlestickChartData_EmptyTestCollection()
        {
            List<TestDTO> testCollection = new();
            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);
            Assert.IsNull(candlesticks, "An empty test collection should result in a null return value. Did not receive a null value!");
        }

        [Test]
        public void GetCandlestickChartData_TooFewTestsInTestCollection()
        {
            List<TestDTO> testCollection = new();
            testCollection.Add(new TestDTO("George", "Physics", "MID-TERM", 68));
            testCollection.Add(new TestDTO("Fred", "Physics", "MID-TERM", 78));
            testCollection.Add(new TestDTO("Henry", "Physics", "MID-TERM", 95));
            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);
            Assert.IsNull(candlesticks, "A test collection with less than 4 tests in each class should result in a null return value. Did not receive a null value!");
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithFourTests()
        {
            List<TestDTO> testCollection = new();
            testCollection.Add(new TestDTO("Bucky Goldstein", "Math 3010", "4D Algorithms", 92));
            testCollection.Add(new TestDTO("Agustus Jones", "Math 3010", "4D Algorithms", 87));
            testCollection.Add(new TestDTO("Scooter McDoolaby", "Math 3010", "4D Algorithms", 61));
            testCollection.Add(new TestDTO("Xander Jamson", "Math 3010", "4D Algorithms", 51));
            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 92, "Expected the candlestick max value to be 92. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 51, "Expected the candlestick min value to be 51. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 56, "Expected the candlestick quartile1 value to be 56. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 89.5, "Expected the candlestick quartile3 value to be 89.5. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithFiveTests()
        {
            List<TestDTO> testCollection = new();
            testCollection.Add(new TestDTO("Bucky Goldstein", "English 2010", "Shakespeare 50,000 word vocabulary", 88));
            testCollection.Add(new TestDTO("Zeke T. Prescott, III", "English 2010", "Shakespeare 50,000 word vocabulary", 71));
            testCollection.Add(new TestDTO("Scooter McDoolaby", "English 2010", "Shakespeare 50,000 word vocabulary", 68));
            testCollection.Add(new TestDTO("Agnes Haversham", "English 2010", "Shakespeare 50,000 word vocabulary", 101));
            testCollection.Add(new TestDTO("Xander Jamson", "English 2010", "Shakespeare 50,000 word vocabulary", 67));

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 101, "Expected the candlestick max value to be 101. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 67, "Expected the candlestick min value to be 67. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 67.5, "Expected the candlestick quartile1 value to be 67.5. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 94.5, "Expected the candlestick quartile3 value to be 94.5. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithEvenWholeOddHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int classSizeSingle = 50;
            int[] testScoresSingle = { 33, 44 };

            for (int i = 1; i <= classSizeSingle; i++)
            {
                int x = i % 2;
                testCollection.Add(new TestDTO("Student_LargeSingle_" + i, "Class_LargeSingleTest", "TestName_Large_Single", testScoresSingle[x]+i));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 93, "Expected the candlestick max value to be 93. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 35, "Expected the candlestick min value to be 35. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 51, "Expected the candlestick quartile1 value to be 51. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 77, "Expected the candlestick quartile3 value to be 77. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithEvenWholeEvenHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int classSizeSingle = 52;
            int[] testScoresSingle = { 33, 44 };

            for (int i = 1; i <= classSizeSingle; i++)
            {
                int x = i % 2;
                testCollection.Add(new TestDTO("Student_LargeSingle_" + i, "Class_LargeSingleTest", "TestName_Large_Single", testScoresSingle[x] + i));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 95, "Expected the candlestick max value to be 95. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 35, "Expected the candlestick min value to be 35. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 52, "Expected the candlestick quartile1 value to be 52. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 78, "Expected the candlestick quartile3 value to be 78. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithOddWholeOddHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int classSizeSingle = 51;
            int[] testScoresSingle = {27, 31, 42, 49};

            for (int i = 1; i <= classSizeSingle; i++)
            {
                int x = i % 4;
                testCollection.Add(new TestDTO("Student_LargeSingle_" + i, "Class_LargeSingleTest", "TestName_Large_Single", testScoresSingle[x] + i));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 100, "Expected the candlestick max value to be 100. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 31, "Expected the candlestick min value to be 31. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 51, "Expected the candlestick quartile1 value to be 51. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 76, "Expected the candlestick quartile3 value to be 76. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_SingleClassWithOddWholeEvenHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int classSizeSingle = 49;
            int[] testScoresSingle = { 27, 31, 42, 49 };

            for (int i = 1; i <= classSizeSingle; i++)
            {
                int x = i % 4;
                testCollection.Add(new TestDTO("Student_LargeSingle_" + i, "Class_LargeSingleTest", "TestName_Large_Single", testScoresSingle[x] + i));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 1, "Expected candlesticks list to contain 1 entry. Actual number returned was: " + candlesticks.Count);
                Assert.IsTrue(candlesticks[0].Max == 96, "Expected the candlestick max value to be 96. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 31, "Expected the candlestick min value to be 31. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 49.5, "Expected the candlestick quartile1 value to be 49.5. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 75.5, "Expected the candlestick quartile3 value to be 75.5. Actual quartile3 value was: " + candlesticks[0].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_MultiClass_OddWholeEvenHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int totalTestCount = 96;
            int[] testScores = { 27, 31, 42, 49 };
            String[] classNamesMulti = { "English", "Math", "Science", "History" };

            for (int i = 1; i <= totalTestCount; i++)
            {
                int x = i % 4;
                testCollection.Add(new TestDTO("Student_" + i, classNamesMulti[x], "TestName_" + classNamesMulti[x], testScores[x] + i));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 4, "Expected candlesticks list to contain 4 entries. Actual number returned was: " + candlesticks.Count);
                // Math
                Assert.IsTrue(candlesticks[0].Title == classNamesMulti[1], "Expected the candlestick title value to be '" + classNamesMulti[1] + "'. Actual title value was: " + candlesticks[0].Title);
                Assert.IsTrue(candlesticks[0].Max == 124, "Expected the candlestick max value to be 124. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 32, "Expected the candlestick min value to be 32. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 54, "Expected the candlestick quartile1 value to be 54. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 102, "Expected the candlestick quartile3 value to be 102. Actual quartile3 value was: " + candlesticks[0].Quartile3);
                // Science
                Assert.IsTrue(candlesticks[1].Title == classNamesMulti[2], "Expected the candlestick title value to be '" + classNamesMulti[2] + "'. Actual title value was: " + candlesticks[1].Title);
                Assert.IsTrue(candlesticks[1].Max == 136, "Expected the candlestick max value to be 136. Actual max value was: " + candlesticks[1].Max);
                Assert.IsTrue(candlesticks[1].Min == 44, "Expected the candlestick min value to be 44. Actual min value was: " + candlesticks[1].Min);
                Assert.IsTrue(candlesticks[1].Quartile1 == 66, "Expected the candlestick quartile1 value to be 66. Actual quartile1 value was: " + candlesticks[1].Quartile1);
                Assert.IsTrue(candlesticks[1].Quartile3 == 114, "Expected the candlestick quartile3 value to be 114. Actual quartile3 value was: " + candlesticks[1].Quartile3);
                // History
                Assert.IsTrue(candlesticks[2].Title == classNamesMulti[3], "Expected the candlestick title value to be '" + classNamesMulti[3] + "'. Actual title value was: " + candlesticks[2].Title);
                Assert.IsTrue(candlesticks[2].Max == 144, "Expected the candlestick max value to be 144. Actual max value was: " + candlesticks[2].Max);
                Assert.IsTrue(candlesticks[2].Min == 52, "Expected the candlestick min value to be 52. Actual min value was: " + candlesticks[2].Min);
                Assert.IsTrue(candlesticks[2].Quartile1 == 74, "Expected the candlestick quartile1 value to be 74. Actual quartile1 value was: " + candlesticks[2].Quartile1);
                Assert.IsTrue(candlesticks[2].Quartile3 == 122, "Expected the candlestick quartile3 value to be 122. Actual quartile3 value was: " + candlesticks[2].Quartile3);
                // English
                Assert.IsTrue(candlesticks[3].Title == classNamesMulti[0], "Expected the candlestick title value to be '" + classNamesMulti[0] + "'. Actual title value was: " + candlesticks[3].Title);
                Assert.IsTrue(candlesticks[3].Max == 123, "Expected the candlestick max value to be 123. Actual max value was: " + candlesticks[3].Max);
                Assert.IsTrue(candlesticks[3].Min == 31, "Expected the candlestick min value to be 31. Actual min value was: " + candlesticks[3].Min);
                Assert.IsTrue(candlesticks[3].Quartile1 == 53, "Expected the candlestick quartile1 value to be 53. Actual quartile1 value was: " + candlesticks[3].Quartile1);
                Assert.IsTrue(candlesticks[3].Quartile3 == 101, "Expected the candlestick quartile3 value to be 101. Actual quartile3 value was: " + candlesticks[3].Quartile3);
            }
        }

        [Test]
        public void GetCandlestickChartData_MultiClass_EvenWholeOddHalfTestSet()
        {
            // Test DTOs for large class single test collection set
            List<TestDTO> testCollection = new();
            int totalTestCount = 100;
            int[] testScores = { 27, 31, 42, 49 };
            String[] classNamesMulti = { "English", "Math", "Science", "History" };

            for (int i = 1; i <= totalTestCount; i++)
            {
                int x = i % 4;
                testCollection.Add(new TestDTO("Student_" + i, classNamesMulti[x], "TestName_" + classNamesMulti[x], testScores[x] + i/2));
            }

            //List<String> csvTestScoresList = GetCsvTestScores(testCollection);

            List<CandlestickDTO>? candlesticks = reportCardService.GetCandlestickChartData(testCollection);

            Assert.IsNotNull(candlesticks, "A test collection with at least 4 tests in each class should result in a valid return value. Received a null response!");

            if (candlesticks != null)
            {
                Assert.IsTrue(candlesticks.Count == 4, "Expected candlesticks list to contain 4 entries. Actual number returned was: " + candlesticks.Count);
                // Math
                Assert.IsTrue(candlesticks[0].Title == classNamesMulti[1], "Expected the candlestick title value to be '" + classNamesMulti[1] + "'. Actual title value was: " + candlesticks[0].Title);
                Assert.IsTrue(candlesticks[0].Max == 79, "Expected the candlestick max value to be 79. Actual max value was: " + candlesticks[0].Max);
                Assert.IsTrue(candlesticks[0].Min == 31, "Expected the candlestick min value to be 31. Actual min value was: " + candlesticks[0].Min);
                Assert.IsTrue(candlesticks[0].Quartile1 == 42, "Expected the candlestick quartile1 value to be 42. Actual quartile1 value was: " + candlesticks[0].Quartile1);
                Assert.IsTrue(candlesticks[0].Quartile3 == 68, "Expected the candlestick quartile3 value to be 68. Actual quartile3 value was: " + candlesticks[0].Quartile3);
                // Science
                Assert.IsTrue(candlesticks[1].Title == classNamesMulti[2], "Expected the candlestick title value to be '" + classNamesMulti[2] + "'. Actual title value was: " + candlesticks[1].Title);
                Assert.IsTrue(candlesticks[1].Max == 91, "Expected the candlestick max value to be 91. Actual max value was: " + candlesticks[1].Max);
                Assert.IsTrue(candlesticks[1].Min == 43, "Expected the candlestick min value to be 43. Actual min value was: " + candlesticks[1].Min);
                Assert.IsTrue(candlesticks[1].Quartile1 == 54, "Expected the candlestick quartile1 value to be 54. Actual quartile1 value was: " + candlesticks[1].Quartile1);
                Assert.IsTrue(candlesticks[1].Quartile3 == 80, "Expected the candlestick quartile3 value to be 80. Actual quartile3 value was: " + candlesticks[1].Quartile3);
                // History
                Assert.IsTrue(candlesticks[2].Title == classNamesMulti[3], "Expected the candlestick title value to be '" + classNamesMulti[3] + "'. Actual title value was: " + candlesticks[2].Title);
                Assert.IsTrue(candlesticks[2].Max == 98, "Expected the candlestick max value to be 98. Actual max value was: " + candlesticks[2].Max);
                Assert.IsTrue(candlesticks[2].Min == 50, "Expected the candlestick min value to be 50. Actual min value was: " + candlesticks[2].Min);
                Assert.IsTrue(candlesticks[2].Quartile1 == 61, "Expected the candlestick quartile1 value to be 61. Actual quartile1 value was: " + candlesticks[2].Quartile1);
                Assert.IsTrue(candlesticks[2].Quartile3 == 87, "Expected the candlestick quartile3 value to be 87. Actual quartile3 value was: " + candlesticks[2].Quartile3);
                // English
                Assert.IsTrue(candlesticks[3].Title == classNamesMulti[0], "Expected the candlestick title value to be '" + classNamesMulti[0] + "'. Actual title value was: " + candlesticks[3].Title);
                Assert.IsTrue(candlesticks[3].Max == 77, "Expected the candlestick max value to be 77. Actual max value was: " + candlesticks[3].Max);
                Assert.IsTrue(candlesticks[3].Min == 29, "Expected the candlestick min value to be 29. Actual min value was: " + candlesticks[3].Min);
                Assert.IsTrue(candlesticks[3].Quartile1 == 40, "Expected the candlestick quartile1 value to be 40. Actual quartile1 value was: " + candlesticks[3].Quartile1);
                Assert.IsTrue(candlesticks[3].Quartile3 == 66, "Expected the candlestick quartile3 value to be 66. Actual quartile3 value was: " + candlesticks[3].Quartile3);
            }
        }

    }
}
