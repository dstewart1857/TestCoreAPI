namespace TestCoreAPI.DTO
{
    public class TestDTO
    {
        public TestDTO()
        { 
            studentName = "studentName";
            className = "className";
            testName = "testName";
            score = 0;
        }

        public TestDTO(String studentName, String className, String testName, int score)
        {
            this.studentName = studentName;
            this.className = className;
            this.testName = testName;
            this.score = score;
        }

        public String studentName { get; set; }
        public String className { get; set; }
        public String testName { get; set; }
        public int score { get; set; }
    }
}
