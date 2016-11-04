using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LoadTestPageGraphModels
    {
        public string PageName { get; set; }
        public List<GraphPloats> Axis { get; set; }

        public LoadTestPageGraphModels()
        {

        }
        public LoadTestPageGraphModels(KeyValuePair<int, List<Entity.LoadTestPageDetail>> entityLoadTestPageDetails, DateTime? loadTestRunStartTime, string instance)
        {
            int index = instance.IndexOf("(");
            if (index > 0)
                instance = instance.Substring(0, index).Replace("{","(").Replace("}",")");
            PageName = instance;
            Axis = new List<GraphPloats>();
            foreach (var entityLoadTestPageDetail in entityLoadTestPageDetails.Value)
            {
                Axis.Add(new GraphPloats(loadTestRunStartTime.Value, entityLoadTestPageDetail.EndTime.Value, entityLoadTestPageDetail.ResponseTime));
            }
        }


        public class GraphPloats
        {
            public double XAxis { get; set; }
            public double YAxis { get; set; }

            public GraphPloats()
            {

            }

            public GraphPloats(DateTime startTime, DateTime endTime, double responseTime)
            {

                TimeSpan span = endTime.Subtract(startTime);
                XAxis = span.Seconds;
                YAxis = responseTime;
            }
        }
    }
}