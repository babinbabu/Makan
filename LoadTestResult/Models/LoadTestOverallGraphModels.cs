using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestOverallGraphModels
    {
        public string Counter { get; set; }
        public string Instance { get; set; }
        public string Category { get; set; }
        public string Computer { get; set; }
        public string Range { get; set; }
        public double Minimum { get; set; }
        public double Maximum { get; set; }
        public double Average { get; set; }

        public LoadTestOverallGraphModels()
        {

        }

        public LoadTestOverallGraphModels(string counter, string category, string computer, double minimum, double maximum, double average, string instance = null)
        {

            Counter = counter;
            Category = category;
            Computer = computer;
            Minimum = minimum;
            Maximum = maximum;
            Average = average;
            Instance = instance;
            Range = Constants.Range.PadRight((int)Math.Floor(Math.Log10(maximum) + 2), '0');
        }

    }


}