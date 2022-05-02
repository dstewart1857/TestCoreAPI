using TestCoreAPI.DTO;

namespace TestCoreAPI.Service
{
    public class ReportCardService
    {
        public ReportCardDTO GradeTest(TestDTO testDTO)
        {
            ReportCardDTO reportCardDTO = new();

            reportCardDTO.StudentName = testDTO.studentName;
            reportCardDTO.ClassName = testDTO.className;
            reportCardDTO.Grade = GetGrade(testDTO.score);

            return reportCardDTO;
        }

        private static string GetGrade(int score)
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
    }
}
