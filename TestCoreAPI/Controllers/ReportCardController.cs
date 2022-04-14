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

        [HttpPost(Name = "gradeTest")]
        public ReportCardDTO gradeTest(TestDTO testDTO)
        {
            return reportCardService.GradeTest(testDTO);
        }

        [HttpPost(Name = "sortGrades")]
        public List<ReportCardDTO> sortGrades(List<TestDTO> testDTOList, int sortMethod)
        {
            return reportCardService.sortGrades(testDTOList, sortMethod);
        }
    }
}
