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
        private List<TestDTO> testCollection = new List<TestDTO>(); 
        

        [HttpPost(Name = "gradeTest")]
        public ReportCardDTO gradeTest(TestDTO testDTO)
        {
            return reportCardService.GradeTest(testDTO);
        }

        [HttpPost(Name = "submitTests")]
        public void submitTests(List<TestDTO> newTests)
        {
            reportCardService.submitTests(newTests, testCollection);
        }

        [HttpGet(Name = "getTests")]
        public List<TestDTO> getTestCollection()
        {
            return testCollection;
        }
    }
}
