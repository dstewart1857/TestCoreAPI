using TestCoreAPI.DTO;

namespace TestCoreAPI.Service
{
    public class ReportCardService
    {
        public ReportCardDTO GradeTest(TestDTO testDTO)
        {
            ReportCardDTO reportCardDTO = new ReportCardDTO();

            reportCardDTO.studentName = testDTO.studentName;
            reportCardDTO.className = testDTO.className;
            reportCardDTO.grade = getGrade(testDTO.score);

            return reportCardDTO;
        }

        private string getGrade(int score)
        {
            return "A";
        }
    }
}
