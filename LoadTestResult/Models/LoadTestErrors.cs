using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestErrors
    {
        public byte ErrorType { get; set; }
        public string ErrorSubtype { get; set; }
        public int ErrorCount { get; set; }
        public string ErrorLastMessage { get; set; }
        public int LoadTestRunId { get; set; }

        public LoadTestErrors()
        {
        }
        public LoadTestErrors(Entity.LoadTestMessage entityLoadTestMessage)
        {

        }
    }
}