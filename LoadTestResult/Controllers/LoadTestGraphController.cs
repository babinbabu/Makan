using LoadTestResult.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    [Authorize]
    public class LoadTestGraphController : Controller
    {
        //
        // GET: /LoadTestGraph/

        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetPageResponseTime(int loadTestRunId)
        {

            List<LoadTestOverallGraphModels> Result = new List<LoadTestOverallGraphModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestPerformanceCounters = db.LoadTestPerformanceCounters.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterId == 74);
                var entityLoadTestPerformanceCounterCategory = db.LoadTestPerformanceCounterCategories.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterCategoryId == entityLoadTestPerformanceCounters.CounterCategoryId);
                var entityLoadTestPerformanceCounterInstances = db.LoadTestPerformanceCounterInstances.Where(x => x.LoadTestRunId == loadTestRunId && x.LoadTestItemId != null).GroupBy(x => x.LoadTestItemId);

                foreach (var entityLoadTestPerformanceCounterInstance in entityLoadTestPerformanceCounterInstances)
                {
                    var instance = entityLoadTestPerformanceCounterInstance.FirstOrDefault(x => x.InstanceName.Contains('}')).InstanceName;
                    int index = instance.IndexOf("(");
                    if (index > 0)
                        instance = instance.Substring(0, index);
                    var entityLoadTestPageSummaryData = db.LoadTestPageSummaryDatas.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.PageId == entityLoadTestPerformanceCounterInstance.Key);
                    Result.Add(new LoadTestOverallGraphModels(entityLoadTestPerformanceCounters.CounterName, entityLoadTestPerformanceCounterCategory.CategoryName, entityLoadTestPerformanceCounterCategory.MachineName, entityLoadTestPageSummaryData.Minimum, entityLoadTestPageSummaryData.Maximum, entityLoadTestPageSummaryData.Average, instance));
                }

                //var result = str.Substring(str.LastIndexOf('/') + 1);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetKeyIndicators(int loadTestRunId)
        {
            List<LoadTestOverallGraphModels> Result = new List<LoadTestOverallGraphModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestPerformanceCounterInstances = db.LoadTestPerformanceCounterInstances
                                                             .Where(x => x.LoadTestRunId == loadTestRunId
                                                              && (x.CounterId == 49 || x.CounterId == 77 ||
                                                                  x.CounterId == 74 || x.CounterId == 72 ||
                                                                  x.CounterId == 91)).GroupBy(x => x.CounterId);
                foreach (var entityLoadTestPerformanceCounterInstance in entityLoadTestPerformanceCounterInstances)
                {
                    var entityLoadTestPerformanceCounter = db.LoadTestPerformanceCounters.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterId == entityLoadTestPerformanceCounterInstance.Key);
                    var entityLoadTestPerformanceCounterCategory = db.LoadTestPerformanceCounterCategories.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterCategoryId == entityLoadTestPerformanceCounter.CounterCategoryId);
                    Result.Add(new LoadTestOverallGraphModels(entityLoadTestPerformanceCounter.CounterName, entityLoadTestPerformanceCounterCategory.CategoryName, entityLoadTestPerformanceCounterCategory.MachineName, entityLoadTestPerformanceCounterInstance.Min(x => x.CumulativeValue).Value, entityLoadTestPerformanceCounterInstance.Max(x => x.CumulativeValue).Value, entityLoadTestPerformanceCounterInstance.Average(x => x.CumulativeValue).Value, entityLoadTestPerformanceCounterInstance.FirstOrDefault().InstanceName));
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetControllerandAgents(int loadTestRunId = 3141)
        {
            List<LoadTestOverallGraphModels> Result = new List<LoadTestOverallGraphModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestPerformanceCounterSamples = db.LoadTestPerformanceCounterSamples.Where(x => x.LoadTestRunId == loadTestRunId && (x.InstanceId == 0 || x.InstanceId == 48)).GroupBy(x => x.InstanceId);
                foreach (var entityLoadTestPerformanceCounterSample in entityLoadTestPerformanceCounterSamples)
                {
                    var entityLoadTestPerformanceCounter = db.LoadTestPerformanceCounters.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterId == entityLoadTestPerformanceCounterSample.Key);
                    var entityLoadTestPerformanceCounterCategory = db.LoadTestPerformanceCounterCategories.FirstOrDefault(x => x.LoadTestRunId == loadTestRunId && x.CounterCategoryId == entityLoadTestPerformanceCounter.CounterCategoryId);
                    Result.Add(new LoadTestOverallGraphModels(entityLoadTestPerformanceCounter.CounterName, entityLoadTestPerformanceCounterCategory.CategoryName, entityLoadTestPerformanceCounterCategory.MachineName, entityLoadTestPerformanceCounterSample.Min(x => x.ComputedValue).Value, entityLoadTestPerformanceCounterSample.Max(x => x.ComputedValue).Value, entityLoadTestPerformanceCounterSample.Average(x => x.ComputedValue).Value));
                }
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        

    }
}
