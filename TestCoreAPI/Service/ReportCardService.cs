using System.Linq;
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
            if (score >= 90)
            {
                return "A";
            }
            else if (score >= 80)
            {
                return "B";
            }
            else if (score >= 70)
            {
                return "C";
            }
            else if (score >= 60)
            {
                return "D";
            }
            else
                return "F";
        }
    }
}
