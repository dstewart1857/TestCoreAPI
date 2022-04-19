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
            List<ReportCardDTO> reportCardDTOs = getReportCardDTOs(testDTOList);

            if (sortMethod == 0)
                return sortByStudentName(reportCardDTOs);
            else
                return sortByGrade(reportCardDTOs);
        }

        public List<ClassAveragesDTO> getAverageMetricsByClass(List<TestDTO> testCollection)
        {
            List<ClassAveragesDTO> classMetrics = new List<ClassAveragesDTO>();
            Dictionary<String, List<TestDTO>> testsByClass = new Dictionary<string, List<TestDTO>>();

            SplitTestsByClass(testCollection, testsByClass);
           
            foreach (var testList in testsByClass.Values)
            {
                classMetrics.Add(getClassAveragesDTO(testList));
            }

            return classMetrics;
        }

        private ClassAveragesDTO getClassAveragesDTO(List<TestDTO> testList)
        {
            List<ReportCardDTO> reportCardList = getReportCardDTOs(testList);
            ClassAveragesDTO classAveragesDTO = new ClassAveragesDTO();
            classAveragesDTO.className = testList.ElementAt(0).className;
            classAveragesDTO.averageScore = (float)testList.Average(test => test.score);
            classAveragesDTO.averageGrade = getGrade((int)classAveragesDTO.averageScore);
            return classAveragesDTO;
        }

        private void SplitTestsByClass(List<TestDTO> testCollection, Dictionary<string, List<TestDTO>> testsByClass)
        {
            testCollection.ForEach(test => insertIntoClassList(test, testsByClass));

        }

        private void insertIntoClassList(TestDTO test, Dictionary<string, List<TestDTO>> testsByClass)
        {
            List<TestDTO> classTests = null;
            try
            {
                classTests = testsByClass[test.className];
            }
            catch (System.Collections.Generic.KeyNotFoundException ex)
            {
                classTests = new List<TestDTO>();
                testsByClass.Add(test.className, classTests);
            }
            
            classTests.Add(test);
        }

        private List<ReportCardDTO> getReportCardDTOs(List<TestDTO> testCollection)
        {
            List<ReportCardDTO> reportCardDTOs = new List<ReportCardDTO>();
            testCollection.ForEach(testDTO => reportCardDTOs.Add(GradeTest(testDTO)));
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
