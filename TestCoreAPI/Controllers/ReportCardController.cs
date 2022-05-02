﻿using Microsoft.AspNetCore.Mvc;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;
using Swashbuckle.AspNetCore.Annotations;

namespace TestCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportCardController : Controller
    {
        private ReportCardService reportCardService = new ReportCardService();
        private static List<TestDTO> testCollection = new List<TestDTO>(); 
        

        [HttpPost(Name = "gradeTest")]
        [SwaggerOperation(Summary = "-- Provides a letter grade for the submitted test score.",
                        Description = "Will return a letter grade of A, B, C, D, or F. Acceptable scores are 0 to 100. Scores outside of this range will return a 'U' for unknown.")]
        public ReportCardDTO gradeTest([FromBody]TestDTO testDTO)
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
