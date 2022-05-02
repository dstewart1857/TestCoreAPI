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
