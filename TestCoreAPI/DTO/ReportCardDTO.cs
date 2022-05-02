namespace TestCoreAPI.DTO
{
    public class ReportCardDTO
    {  
        public ReportCardDTO()
        {
            StudentName = "studentName";
            ClassName = "className";
            Grade = "";
        }

        public String StudentName { get; set; }
        public String ClassName { get; set; }
        public String Grade { get; set; }
    }
}
