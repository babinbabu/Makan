using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestOverallResultsModels
    {
        public int MaxUserLoad { get; set; }
        public float TestsPerSec { get; set; }
        public float TestsFailed { get; set; }
        public float AvgTestTime { get; set; }
        public float Transactions { get; set; }
        public float AvgTransactionTime { get; set; }
        public float Pages { get; set; }
        public float AvgPageTime { get; set; }
        public float Requests { get; set; }
        public float RequestsFailed { get; set; }
        public float RequestsCachedPercentage { get; set; }
        public float AvgResponseTime { get; set; }
        public float AvgContentLength { get; set; }

        public LoadTestOverallResultsModels()
        {
        }
    }
}