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

        public List<ReportCardDTO> sortGrades(List<TestDTO> testDTOList, int sortMethod)
        {
            List<ReportCardDTO> reportCardDTOs = new List<ReportCardDTO>();

            testDTOList.ForEach(testDTO => reportCardDTOs.Add(GradeTest(testDTO)));

            if (sortMethod == 0)
                return sortByStudentName(reportCardDTOs);
            else
                return sortByGrade(reportCardDTOs);

            return reportCardDTOs;
        }

        private List<ReportCardDTO> sortByStudentName(List<ReportCardDTO> reportCardDTOs)
        {
            return reportCardDTOs.OrderBy(reportCardDTO => reportCardDTO.studentName).ToList();
        }

        private List<ReportCardDTO> sortByGrade(List<ReportCardDTO> reportCardDTOs)
        {
            return reportCardDTOs.OrderBy(reportCardDTO=> reportCardDTO.grade).ToList();
        }
    }
}
