using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class PageResultsModels
    {
        public string URL { get; set; }
        public string Scenario { get; set; }
        public string TestName { get; set; }
        public double AvgPageTime { get; set; }
        public int PageCount { get; set; }
        public OverallThresholdRuleResult ThresholdRuleResult { get; set; }

        public PageResultsModels()
        {

        }
        public PageResultsModels(Entity.LoadTestPageSummaryData entityLoadTestPageSummaryData,Entity.db_LoadTest2010Entities db)
        {
            var entityWebLoadTestRequestMap = db.WebLoadTestRequestMaps.FirstOrDefault(x => x.RequestId == entityLoadTestPageSummaryData.PageId);
            if (entityWebLoadTestRequestMap != null)
            {
                URL = entityWebLoadTestRequestMap.RequestUri;
            }
            PageCount = entityLoadTestPageSummaryData.PageCount;
            AvgPageTime = entityLoadTestPageSummaryData.Average;
            Scenario = db.LoadTestScenarios.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestPageSummaryData.LoadTestRunId).ScenarioName;
            TestName = db.LoadTestCases.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestPageSummaryData.LoadTestRunId).TestCaseName;
            var entityLoadTestPerformanceCounterInstances = db.LoadTestPerformanceCounterInstances.Where(x => x.LoadTestRunId == entityLoadTestPageSummaryData.LoadTestRunId && x.LoadTestItemId == entityLoadTestPageSummaryData.PageId);
            bool overallThresholdRuleResultStatus = true;
            if (entityLoadTestPerformanceCounterInstances.Any(x => x.OverallThresholdRuleResult == 2) && overallThresholdRuleResultStatus)
            {
                ThresholdRuleResult = OverallThresholdRuleResult.critical;
                overallThresholdRuleResultStatus = false;
            }
            else if (entityLoadTestPerformanceCounterInstances.Any(x => x.OverallThresholdRuleResult == 1) && overallThresholdRuleResultStatus)
            {
                ThresholdRuleResult = OverallThresholdRuleResult.warnings;
                overallThresholdRuleResultStatus = false;
            }
            else
            {
                ThresholdRuleResult = OverallThresholdRuleResult.ok;
            }

        }

    }


}