using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class TestResultsModels
    {
        public string Name { get; set; }
        public string Scenario { get; set; }
        public int TotalTests { get; set; }
        public string FailedTests { get; set; }
        public double AvgTestTime { get; set; }

        public TestResultsModels()
        {

        }
        public TestResultsModels(Entity.LoadTestTestSummaryData entityLoadTestTestSummaryData,Entity.db_LoadTest2010Entities db)
        {
            Name = db.LoadTestCases.FirstOrDefault(x => x.TestCaseId == entityLoadTestTestSummaryData.TestCaseId && x.LoadTestRunId == entityLoadTestTestSummaryData.LoadTestRunId).TestCaseName;
            Scenario = db.LoadTestScenarios.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestTestSummaryData.LoadTestRunId).ScenarioName;
            TotalTests = entityLoadTestTestSummaryData.TestsRun;          
            int failedTests=db.LoadTestMessages.Where(x=>x.LoadTestRunId==entityLoadTestTestSummaryData.LoadTestRunId && x.MessageType==2 && x.TestCaseId==entityLoadTestTestSummaryData.TestCaseId).Count();
            FailedTests = string.Format("{0} ({1})", failedTests, failedTests * 100 / TotalTests);
            AvgTestTime = entityLoadTestTestSummaryData.Average;
        }

    }
}