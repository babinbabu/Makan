using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestNameModels
    {
        public string TestName { get; set; }
        public string TestFilePath { get; set; }

        public LoadTestNameModels()
        {

        }

        public LoadTestNameModels(string CurrentTestName, string filePath)
        {
            TestName = CurrentTestName;
            TestFilePath = filePath;
        }
    }
}