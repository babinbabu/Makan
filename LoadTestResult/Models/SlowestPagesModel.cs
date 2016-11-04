using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class SlowestPagesModel
    {
        public string URL { get; set; }
        public double? PageTime { get; set; }

        public SlowestPagesModel()
        {
        }
        public SlowestPagesModel(Entity.LoadTestPageSummaryData entityLoadTestPageSummaryData,Entity.db_LoadTest2010Entities db)
        {
            var entityWebLoadTestRequestMap = db.WebLoadTestRequestMaps.FirstOrDefault(x => x.RequestId == entityLoadTestPageSummaryData.PageId);
            if (entityWebLoadTestRequestMap != null)
            {
                URL = entityWebLoadTestRequestMap.RequestUri;
            }
            PageTime = entityLoadTestPageSummaryData.Percentile95;
        }
    }
}