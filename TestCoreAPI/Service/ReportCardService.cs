using TestCoreAPI.DTO;
using System.Runtime.CompilerServices;
using System.Net;

[assembly: InternalsVisibleTo("TestCoreAPITests")]
namespace TestCoreAPI.Service
{
    public class ReportCardService
    {
        public ReportCardDTO GradeTest(TestDTO testDTO)
        {
            ReportCardDTO reportCardDTO = new();

            reportCardDTO.StudentName = testDTO.studentName;
            reportCardDTO.ClassName = testDTO.className;
            reportCardDTO.Grade = GetGrade(testDTO.score);

            return reportCardDTO;
        }

        internal string GetGrade(int score)
        {
            String grade;

            switch(score)
            {
                case >= 90 and <= 100:
                    {
                        grade = "A";
                        break;
                    }
                case >= 80 and < 90: 
                    {
                        grade = "B";
                        break;
                    }
                case >= 70 and < 80:
                    {
                        grade = "C";
                        break;
                    }
                case >= 60 and < 70:
                    {
                        grade = "D";
                        break;
                    }
                case >= 0 and < 60:
                    {
                        grade = "F";
                        break;
                    }
                default:
                    {
                        grade = "U";
                        break;
                    }
            }
            return grade;

        }

        internal List<CandlestickDTO> GetCandlestickChartData(List<TestDTO> testCollection)
        {
            if(testCollection.Count == 0)
            {
                throw new ArgumentException("The test collection was empty!\nCandlestick chart data connot be provided unless the test collection contains at least 4 test results for each class!");
            }

            List<CandlestickDTO> candlestickData = new();
            List<string> classNames = new();

            // Get list of class names in testCollection
            foreach (TestDTO testDTO in testCollection)
            {
                if(!classNames.Contains(testDTO.className))
                {
                    classNames.Add(testDTO.className);
                }
            }

            // Get scores for each class and calculate quartiles
            foreach(string className in classNames)
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

                // Check if at least 4 test scores exist for the class
                if(classTestScores.Count < 4)
                {
                    throw new ArgumentException("Did not find at least 4 tests for the " + className + " class!\nCandlestick chart data connot be provided unless the test collection contains at least 4 test results for each class!");
                }

                classTestScores.Sort();

                CandlestickDTO candlestickDTO = GetQuartiles(classTestScores.ToArray());
                candlestickDTO.Title = className;
                candlestickData.Add(candlestickDTO);
            }

            return candlestickData;
        }

        internal CandlestickDTO GetQuartiles(int[] classTestScores)
        {
            int numOfTestScores = classTestScores.Length;
            int middleIndex = numOfTestScores / 2; //Index of middle test score
            int halfMidIndex = middleIndex / 2;  // Index of half of test scores

            float firstQuartile;
            float thirdQuartile;

            if (numOfTestScores % 2 == 0)
            {
                //********** Even number of elements in the test collection **********

                if (middleIndex % 2 == 0)
                {
                    // Even number of elements in halves - calculate average of two middle elements
                    firstQuartile = (float)(classTestScores[halfMidIndex - 1] + classTestScores[halfMidIndex]) / 2;
                    thirdQuartile = (float)(classTestScores[middleIndex + halfMidIndex - 1] + classTestScores[middleIndex + halfMidIndex]) / 2;
                }
                else
                {
                    // Odd number of elements in halves
                    firstQuartile = classTestScores[halfMidIndex];
                    thirdQuartile = classTestScores[halfMidIndex + middleIndex];
                }
            }
            else
            {
                //********** Odd number of elements in the test collection **********

                if (middleIndex % 2 == 0)
                {
                    // Even number of elements in halves - calculate average of two middle elements
                    firstQuartile = (float)(classTestScores[halfMidIndex - 1] + classTestScores[halfMidIndex]) / 2;
                    thirdQuartile = (float)(classTestScores[middleIndex + halfMidIndex] + classTestScores[middleIndex + halfMidIndex + 1]) / 2;
                }
                else
                {
                    // Odd number of elements in halves
                    firstQuartile = classTestScores[halfMidIndex];
                    thirdQuartile = classTestScores[halfMidIndex + middleIndex + 1];
                }
            }

            float max = classTestScores[classTestScores.Length - 1];
            CandlestickDTO candlestickDTO = new("", max, thirdQuartile, firstQuartile, classTestScores[0]);
            return candlestickDTO;
        }



    }
}
