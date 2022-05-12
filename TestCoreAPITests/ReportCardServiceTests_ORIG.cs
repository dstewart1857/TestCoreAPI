using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestCoreAPI.DTO;
using TestCoreAPI.Service;


namespace TestCoreAPITests
{
    internal class ReportCardServiceTests_ORIG
    {
        public List<TestDTO> testDTOs = new List<TestDTO>();
        public ReportCardService reportCardService = new ReportCardService();

        [SetUp]
        public void Setup()
        {
            initializeTestDTOs(testDTOs);
        }

        private void initializeTestDTOs(List<TestDTO> testDTOs)
        {
            testDTOs.Add(getTestDTO("English 2010", "Bucky Goldstein", "Shakespeare's 50K Word Vocabulary", 88));
            testDTOs.Add(getTestDTO("English 2010", "Zeke T. Prescott, III", "Shakespeare's 50K Word Vocabulary", 61));
            testDTOs.Add(getTestDTO("English 2010", "Sally Jessie Raphael", "Shakespeare's 50K Word Vocabulary", 74));
            testDTOs.Add(getTestDTO("English 2010", "JoJo McFarland", "Shakespeare's 50K Word Vocabulary", 31));
            testDTOs.Add(getTestDTO("Math 3010", "Bucky Goldstein", "4D Algorithims", 98));
            testDTOs.Add(getTestDTO("Math 3010", "Zeke T. Prescott, III", "4D Algorithims", 67));
            testDTOs.Add(getTestDTO("Math 3010", "Wicket W. Warkick", "4D Algorithims", 84));
        }

        private TestDTO getTestDTO(String className, String studentName, String testName, int score)
        {
            TestDTO testDTO = new TestDTO();
            testDTO.className = className;
            testDTO.studentName = studentName;
            testDTO.testName = testName;
            testDTO.score = score;
            return testDTO;
        }

    }
}
