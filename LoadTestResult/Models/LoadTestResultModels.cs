using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestResultModels
    {
        public string LoadTestName { get; set; }
        public int LoadTestRunId { get; set; }
        public string RunId { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int RunDuration { get; set; }
        public int WarmupTime { get; set; }
        public string RunSettingUsed { get; set; }
        public bool IsLocalRun { get; set; }
        public string Outcome { get; set; }
        public string OverallThresholdRuleResults { get; set; }
        public int NumberOfAgents { get; set; }
        public int MaxUserLoad { get; set; }
        public float TestsPerSec { get; set; }
        public int TestsFailed { get; set; }
        public double AvgTestTime { get; set; }
        public double? TransactionsPerSec { get; set; }
        public double AvgTransactionTime { get; set; }
        public double PagesPerSec { get; set; }
        public double AvgPageTime { get; set; }
        public int RequestsFailed { get; set; }
        public double AvgResponseTime { get; set; }
        public string SlowestTestName { get; set; }
        public double? SlowestTestTime { get; set; }
        public int TotalTests { get; set; }
        public List<LoadTestErrors> LoadTestErrors { get; set; }
        public List<PageResultsModels> PageResults { get; set; }
        public List<SlowestPagesModel> SlowestPages { get; set; }
        public List<TestResultsModels> TestResults { get; set; }
        public SlowestTestsModel SlowestTest { get; set; }
        public LoadTestResultModels()
        {

        }

        public LoadTestResultModels(Entity.LoadTestRun entityLoadTestRun, Entity.db_LoadTest2010Entities db, bool compare = false)
        {
            RunId = entityLoadTestRun.RunId;
            LoadTestRunId = entityLoadTestRun.LoadTestRunId;
            LoadTestName = entityLoadTestRun.LoadTestName;
            Description = entityLoadTestRun.Description;
            StartTime = entityLoadTestRun.StartTime;
            EndTime = entityLoadTestRun.EndTime;
            RunDuration = entityLoadTestRun.RunDuration;
            WarmupTime = entityLoadTestRun.WarmupTime;
            RunSettingUsed = entityLoadTestRun.RunSettingUsed;
            IsLocalRun = entityLoadTestRun.IsLocalRun;
            Outcome = entityLoadTestRun.Outcome;
            NumberOfAgents = entityLoadTestRun.LoadTestRunAgents.Select(e => e.AgentId).Distinct().Count();
            SlowestTestName = db.LoadTestCases.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).TestCaseName;
            SlowestTestTime = db.LoadTestTestSummaryDatas.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).OrderByDescending(e => e.Percentile95).Take(5).Sum(p => p.Percentile95);
            MaxUserLoad = db.LoadTestTestDetails.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).Select(e => e.UserId).Distinct().Count();

            var entityLoadTestTestSummaryDatas = db.LoadTestTestSummaryDatas.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).ToList();
            if (entityLoadTestTestSummaryDatas.Count > 0)
            {
                TestsPerSec = (float)entityLoadTestTestSummaryDatas.Sum(x => x.TestsRun) / entityLoadTestRun.RunDuration;
            }

            TestsFailed = entityLoadTestRun.LoadTestMessages.Where(x => x.MessageType == 2).Count();
            AvgTestTime = db.LoadTestTestSummaryDatas.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).Sum(x => x.Average);
            PagesPerSec = (double)entityLoadTestRun.LoadTestPageSummaryDatas.Sum(x => x.PageCount) / entityLoadTestRun.RunDuration;
            AvgPageTime = (double)entityLoadTestRun.LoadTestPageSummaryDatas.Sum(x => x.Average) / entityLoadTestRun.LoadTestPageSummaryDatas.Count();
            var entityLoadTestTransactionSummaryData = db.LoadTestTransactionSummaryDatas.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId);
            if (entityLoadTestTransactionSummaryData != null)
            {
                TransactionsPerSec = (double)entityLoadTestTransactionSummaryData.TransactionCount / entityLoadTestRun.RunDuration;
                AvgTransactionTime = entityLoadTestTransactionSummaryData.Average;
            }

            RequestsFailed = entityLoadTestRun.LoadTestMessages.Select(x => x.RequestId).Distinct().Count();
            AvgResponseTime = db.LoadTestPageDetails.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).Average(x => x.ResponseTime);
            //TotalTests = db.LoadTestTestSummaryDatas.Where(x => x.LoadTestRunId == entityLoadTestRun.LoadTestRunId).Sum(x => x.TestsRun);
            if (compare)
            {
                LoadTestErrors = new List<LoadTestErrors>();
                if (entityLoadTestRun.LoadTestMessages != null)
                {
                    foreach (var LoadTestError in entityLoadTestRun.LoadTestMessages.GroupBy(p => p.SubType).Select(g => new { SubType = g.Key, Count = g.Count(), MessageText = g.FirstOrDefault().MessageText, ErrorType = g.FirstOrDefault().MessageType, LoadTestRunId = g.FirstOrDefault().LoadTestRunId }))
                    {
                        LoadTestErrors.Add(new LoadTestErrors
                        {
                            ErrorSubtype = LoadTestError.SubType,
                            ErrorCount = LoadTestError.Count,
                            ErrorLastMessage = LoadTestError.MessageText,
                            ErrorType = LoadTestError.ErrorType,
                            LoadTestRunId = LoadTestError.LoadTestRunId,

                        });
                    }
                }
                PageResults = new List<PageResultsModels>();
                if (entityLoadTestRun.LoadTestPageSummaryDatas != null)
                {
                    foreach (var LoadTestPageSummaryData in entityLoadTestRun.LoadTestPageSummaryDatas.OrderByDescending(x => x.Average))
                    {
                        PageResults.Add(new PageResultsModels(LoadTestPageSummaryData, db));
                    }
                }
                SlowestPages = new List<SlowestPagesModel>();
                if (entityLoadTestRun.LoadTestPageSummaryDatas != null)
                {
                    foreach (var LoadTestPageSummaryData in entityLoadTestRun.LoadTestPageSummaryDatas.OrderByDescending(x => x.Percentile95).Take(5))
                    {
                        SlowestPages.Add(new SlowestPagesModel(LoadTestPageSummaryData, db));
                    }
                }
                TestResults = new List<TestResultsModels>();

                if (entityLoadTestTestSummaryDatas != null)
                {
                    foreach (var entityLoadTestTestSummaryData in entityLoadTestTestSummaryDatas)
                    {
                        TestResults.Add(new TestResultsModels(entityLoadTestTestSummaryData, db));
                    }
                }
            }

        }
    }
    public class LoadTestResultList : LoadTestResultModels
    {

        public LoadTestResultList()
        {

        }

        public LoadTestResultList(Entity.LoadTestRun entityLoadTestRun)
        {

            LoadTestName = entityLoadTestRun.LoadTestName;
            LoadTestRunId = entityLoadTestRun.LoadTestRunId;
            RunId = entityLoadTestRun.RunId;
            Description = entityLoadTestRun.Description;
            StartTime = entityLoadTestRun.StartTime;
            EndTime = entityLoadTestRun.EndTime;
            RunDuration = entityLoadTestRun.RunDuration;
            WarmupTime = entityLoadTestRun.WarmupTime;
            RunSettingUsed = entityLoadTestRun.RunSettingUsed;
            IsLocalRun = entityLoadTestRun.IsLocalRun;
            Outcome = entityLoadTestRun.Outcome;
            bool OverallThresholdRuleResultStatus = true;
            if (entityLoadTestRun.LoadTestPerformanceCounterInstances.Any(x => x.OverallThresholdRuleResult == (byte?)OverallThresholdRuleResult.critical && x.LoadTestItemId!=null) && OverallThresholdRuleResultStatus)
            {
                OverallThresholdRuleResults = OverallThresholdRuleResult.critical.ToString();
                OverallThresholdRuleResultStatus = false;
            }
            if (entityLoadTestRun.LoadTestPerformanceCounterInstances.Any(x => x.OverallThresholdRuleResult == (byte?)OverallThresholdRuleResult.warnings && x.LoadTestItemId!=null) && OverallThresholdRuleResultStatus)
            {
                OverallThresholdRuleResults = OverallThresholdRuleResult.warnings.ToString();
                OverallThresholdRuleResultStatus = false;
            }
            if (entityLoadTestRun.LoadTestPerformanceCounterInstances.Any(x => x.OverallThresholdRuleResult == (byte?)OverallThresholdRuleResult.ok) && OverallThresholdRuleResultStatus)
            {
                OverallThresholdRuleResults = OverallThresholdRuleResult.ok.ToString();
                OverallThresholdRuleResultStatus = false;
            }

        }
    }

    public class ComapreLoadTestResults
    {
        List<LoadTestResultModels> LoadTestResult { get; set; }
        public ComapreLoadTestResults()
        {
        }
        public ComapreLoadTestResults(List<LoadTestResultModels> loadTestResult)
        {
            LoadTestResult = loadTestResult;
        }
    }
}