using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportCardController : Controller
    {
        private ReportCardService reportCardService = new();
        private static List<TestDTO> testCollection = new();

        [HttpPost(Name = "gradeTest")]
        [SwaggerOperation(Summary = "-- Provides a letter grade for the submitted test score.",
                        Description = "Will return a letter grade of A, B, C, D, or F. Acceptable scores are 0 to 100. Scores outside of this range will return a 'U' for unknown.")]
        public ReportCardDTO GradeTest([FromBody]TestDTO testDTO)
        {
            return reportCardService.GradeTest(testDTO);
        }

        [Route("submitTests")]
        [HttpPost]
        [SwaggerOperation(Summary = "-- Adds test objects to a test collection.",
                        Description = "Builds a collection of test objects that can be retrieved using the 'getTests' endpoint.")]
        public void SubmitTests([FromBody]List<TestDTO> newTests)
        {
            testCollection.AddRange(newTests);
        }

        [Route("getTests")]
        [HttpGet]
        [SwaggerOperation(Summary = "-- Gets a list of test objects from a previously built test collection.",
                        Description = "Returns the list of test objects stored in the collection created by using the 'submitTests' endpoint.")]
        public List<TestDTO> GetTestCollection()
        {
            return testCollection;
        }

        [Route("deleteAllTests")]
        [HttpDelete]
        [SwaggerOperation(Summary = "-- Deletes all test objects in the current test collection.",
                        Description = "This will delete ALL tests that exist in the current test collection.")]
        public void DeleteTestCollection()
        {
            testCollection.Clear();
        }

        [Route("sortedGrades")]
        [HttpGet]
        [SwaggerOperation(Summary = "-- Returns a list of sorted tests by student name or grade. The tests are first graded and then sorted.",
                        Description = "The sort parameter will determine which sort method is used. A value of 0 will sort by student name in ascending order (A..Z). A value of 1 will sort by grade in ascending order (A,B,C,D,F).")]
        public ActionResult<List<ReportCardDTO>> SortedGrades(int sortMethod)
        {
            List<ReportCardDTO> reportCardCollection = new();

            foreach(TestDTO testDTO in testCollection)
            {
                ReportCardDTO reportCardDTO = reportCardService.GradeTest(testDTO);
                reportCardCollection.Add(reportCardDTO);
            }

            // Sort reportCardCollection: 0 = sort by student name, 1 = sort by grade
            List<ReportCardDTO> sortedReportCards = new();
            if(sortMethod == 0)
            {
                // Sort by student name in ascending order(A..Z)
                 sortedReportCards = reportCardCollection.OrderBy(o => o.StudentName).ToList();
            }
            else if(sortMethod == 1)
            {
                // Sort by grade in ascending order (A,B,C,D,F)
                sortedReportCards = reportCardCollection.OrderBy(o => o.Grade).ToList();
            }
            else
            {
                return BadRequest("The provided sort method '" + sortMethod + "' is not supported!  Please select one of the available sort methods:\n0 = sort by name (ascending)\n1 = sort by grade (ascending)");
            }

            return Ok(sortedReportCards);
        }

        [Route("getAverageMetricsByClass")]
        [HttpGet]
        [SwaggerOperation(Summary = "-- Will analyze the data in the test collection and return a list of classes with the average score and grade.",
                        Description = "The response for each class will be returned in a ClassAveragesDTO object which contains the className, averageGrade and averageScore fields.")]
        public List<ClassAveragesDTO> GetAverageMetricsByClass()
        {
            List<ClassAveragesDTO> classAverageCollection = new();
            Dictionary<string, int> classScoresTotal = new();

            // Get list of test scores and build dictionary of class name (key) / score (value)
            foreach (TestDTO testDTO in testCollection)
            {
                if (classScoresTotal.ContainsKey(testDTO.className))
                {
                    classScoresTotal[testDTO.className] += testDTO.score;
                }
                else
                {
                    classScoresTotal.Add(testDTO.className, testDTO.score);
                }
            }

            // Determine average score and grade for each class
            foreach (string key in classScoresTotal.Keys)
            {
                float avgScore = (float)classScoresTotal[key] / (float)testCollection.FindAll(x => x.className == key).Count;
                string avgGrade = reportCardService.GetGrade((int)(avgScore + 0.5));
                classAverageCollection.Add(new ClassAveragesDTO(key, avgGrade, avgScore));
            }

            return classAverageCollection;
        }

        [Route("getCandlestickChartData")]
        [HttpGet]
        [SwaggerOperation(Summary = "-- Uses the current list of tests in the test collection and calculates the min, max, Q1, and Q3 values for each test.",
                        Description = "Calculates the min, max, 1st and 3rd quartiles for each set of test scores in the test collection. Results are returned as a CandlestickDTO with the test name and the four values.")]
        public ActionResult<List<CandlestickDTO>> GetCandlestickChartData()
        {
            List<CandlestickDTO>? candlestickCollection = reportCardService.GetCandlestickChartData(testCollection);

            if (testCollection.Count < 4 || candlestickCollection == null)
            {
                return BadRequest(error: "Candlestick chart data connot be provided unless the test collection contains at least 4 test results for each class!");
            }

            return candlestickCollection;
        }
    }
}
