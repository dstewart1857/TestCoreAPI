namespace TestCoreAPI.DTO
{
    public class ReportCardDTO
    {  
        public ReportCardDTO()
        {
            studentName = "studentName";
            className = "className";
            grade = "";
        }

        public String studentName { get; set; }
        public String className { get; set; }
        public String grade { get; set; }
    }
}
