using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportCardController : Controller
    {
        private ReportCardService reportCardService = new ReportCardService();
        private static List<TestDTO> testCollection = new List<TestDTO>(); 
        

        [HttpPost(Name = "gradeTest")]
        public ReportCardDTO gradeTest(TestDTO testDTO)
        {
            return reportCardService.GradeTest(testDTO);
        }

        [Route("submitTests")]
        [HttpPost]
        public void submitTests([FromBody]List<TestDTO> newTests)
        {
        }

        [Route("getTests")]
        [HttpGet]
        public List<TestDTO> getTestCollection()
        {
            return null;
        }

        [Route("sortedGrades")]
        [HttpGet]
        public List<ReportCardDTO> sortedGrades(int sortMethod)
        {
            return null;
        }
    }
}
