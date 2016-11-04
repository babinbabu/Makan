using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class DetailedErrorModels
    {
        public string Agent { get; set; }
        public string Test { get; set; }
        public string Scenario { get; set; }
        public string Request { get; set; }
        public byte Type { get; set; }
        public string SubType { get; set; }
        public string Text { get; set; }
        public int LoadTestRunId { get; set; }

        public DetailedErrorModels()
        {
        }

        public DetailedErrorModels(Entity.LoadTestMessage entityLoadTestMessage,Entity.db_LoadTest2010Entities db)
        {
            LoadTestRunId = entityLoadTestMessage.LoadTestRunId;
            Type = entityLoadTestMessage.MessageType;
            SubType = entityLoadTestMessage.SubType;
            Text = entityLoadTestMessage.MessageText;
            Scenario = db.LoadTestScenarios.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestMessage.LoadTestRunId).ScenarioName;
            Agent = db.LoadTestRunAgents.FirstOrDefault(x => x.LoadTestRunId == entityLoadTestMessage.LoadTestRunId).AgentName;
            Request = entityLoadTestMessage.LoadTestRun.WebLoadTestRequestMaps.FirstOrDefault(x => x.RequestId == entityLoadTestMessage.RequestId).RequestUri;
            Test = entityLoadTestMessage.LoadTestRun.LoadTestCases.FirstOrDefault(x => x.TestCaseId == entityLoadTestMessage.TestCaseId).TestCaseName;
        }


    }
}