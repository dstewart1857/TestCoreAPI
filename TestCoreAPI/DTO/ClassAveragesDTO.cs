namespace TestCoreAPI.DTO
{
    public class ClassAveragesDTO
    {
        public ClassAveragesDTO(string className, string averageGrade, float averageScore)
        {
            ClassName = className;
            AverageGrade = averageGrade;
            AverageScore = averageScore;
        }

        public String ClassName { get; set; }
        public String AverageGrade { get; set; }
        public float AverageScore { get; set; }
    }
}
