using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class SlowestTestsModel
    {
        public string SlowestTestName { get; set; }
        public double? SlowestTestTime { get; set; }

        public SlowestTestsModel()
        {
        }
        public SlowestTestsModel(Entity.LoadTestTestSummaryData entityLoadTestTestSummaryData)
        {
        }
    }
}