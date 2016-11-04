using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadListModels
    {
        public string LoadTestName { get; set; }

        public LoadListModels()
        {
        }

        public LoadListModels(string testName)
        {
            LoadTestName = testName;
        }
    }
}