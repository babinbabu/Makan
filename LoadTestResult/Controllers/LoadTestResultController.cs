using LoadTestResult.Models;
using LoadTestResult.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    [Authorize]
    public class LoadTestResultController : Controller
    {
        //
        // GET: /LoadTestResult/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            List<LoadListModels> Results = new List<LoadListModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                foreach (var result in db.LoadTestRuns.Select(e => e.LoadTestName).Distinct())
                {
                    Results.Add(new LoadListModels(result));
                }
            }
            return Json(Results, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadTestBasedOnName(string loadTestName, int page = 1, int itemsPerPage = 20)
        {
            long TotalRecord = 0;
            var Results = LoadTestService.GetLoadTestBasedOnName(loadTestName, itemsPerPage, (page - 1) * itemsPerPage, out TotalRecord);
            return Json(new
            {
                data = Results,
                TotalRecord = TotalRecord
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadTestBasedOnId(int LoadTestRunId)
        {

            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var entityLoadTestRuns = db.LoadTestRuns.FirstOrDefault(e => e.LoadTestRunId == LoadTestRunId);
                if (entityLoadTestRuns != null)
                {
                    return Json(new LoadTestResultModels(entityLoadTestRuns, db, true), JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false);
                }
            }

        }

        public ActionResult DetailedErrorMessage(int LoadTestRunId, string SubType)
        {

            var entityLoadTestMessages = LoadTestService.GetDetailedErrorMessage(LoadTestRunId, SubType);
            return Json(entityLoadTestMessages, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLoadTestPageGraphValues(int LoadTestRunId)
        {

            List<LoadTestPageGraphModels> Result = new List<LoadTestPageGraphModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                var LoadTestRunStartTime = db.LoadTestRuns.FirstOrDefault(x => x.LoadTestRunId == LoadTestRunId).StartTime;
                var entityLoadTestPageDetails = db.LoadTestPageDetails.Where(x => x.LoadTestRunId == LoadTestRunId && x.Outcome==0).GroupBy(x => x.PageId);
                foreach (var entityLoadTestPageDetail in entityLoadTestPageDetails)
                {
                    KeyValuePair<int, List<Entity.LoadTestPageDetail>> collection = new KeyValuePair<int, List<Entity.LoadTestPageDetail>>(entityLoadTestPageDetail.Key, entityLoadTestPageDetail.ToList());
                    var entityLoadTestPerformanceCounterInstances = db.LoadTestPerformanceCounterInstances.Where(x => x.LoadTestItemId == entityLoadTestPageDetail.Key && x.LoadTestRunId == LoadTestRunId);
                    Result.Add(new LoadTestPageGraphModels(collection, LoadTestRunStartTime, entityLoadTestPerformanceCounterInstances.FirstOrDefault(x => x.InstanceName.Contains("}")).InstanceName));
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetResultCompare(List<int> LoadTestRunIds)
        {

            List<LoadTestResultModels> Results = new List<LoadTestResultModels>();
            using (Entity.db_LoadTest2010Entities db = new Entity.db_LoadTest2010Entities())
            {
                foreach (var LoadTestRunId in LoadTestRunIds)
                {
                    var entityLoadTestRuns = db.LoadTestRuns.FirstOrDefault(e => e.LoadTestRunId == LoadTestRunId);
                    if (entityLoadTestRuns != null)
                    {
                        Results.Add(new LoadTestResultModels(entityLoadTestRuns, db, true));
                    }
                }


            }

            return Json(Results, JsonRequestBehavior.AllowGet);
        }
    }
}
