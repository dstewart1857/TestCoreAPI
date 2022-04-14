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
        public ReportCardDTO gradeTest([FromBody]TestDTO testDTO)
        {
            return reportCardService.GradeTest(testDTO);
        }
    }
}
