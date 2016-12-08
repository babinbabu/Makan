using LoadTestResult.Models;
using LoadTestResult.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    public class LogErrorController : Controller
    {
        //
        // GET: /LogError/

        [HttpPost]
        public JsonResult LogErrorDateWise(DateTime logErrorDate, DateTime? fromTime, DateTime? toTime, int page = 1, int itemsPerPage = 20)
        {
            CloudStorageAccount storageAccount =
                  CloudStorageAccount.Parse("DefaultEndpointsProtocol=" + Constants.Protocol + ";AccountName=" + Constants.AccountName + ";AccountKey=" + Constants.AccountKey);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference("Log" + logErrorDate.ToString("ddMMyyyy"));
            List<LogEventEntityModels> logEventEntityQuerys = new List<LogEventEntityModels>();
            if (table.Exists())
            {
                logEventEntityQuerys = (from entry in table.CreateQuery<LogEventEntityModels>()
                                        select entry).ToList();
                if (fromTime.HasValue && toTime.HasValue)
                {
                    DateTime logErrorFromDate = logErrorDate.AddHours(fromTime.Value.Hour).AddMinutes(fromTime.Value.Minute);
                    DateTime logErrorToDate = logErrorDate.AddHours(toTime.Value.Hour).AddMinutes(toTime.Value.Minute);

                    logEventEntityQuerys = logEventEntityQuerys.Where(x => x.Timestamp.LocalDateTime >= logErrorFromDate && x.Timestamp.LocalDateTime <= logErrorToDate).ToList();
                }

                long TotalRecord = logEventEntityQuerys.Count();


                logEventEntityQuerys = logEventEntityQuerys.OrderByDescending(e => e.Timestamp).Skip((page - 1) * itemsPerPage).Take(itemsPerPage).ToList();

                return Json(new
                {
                    data = logEventEntityQuerys,
                    TotalRecord = TotalRecord
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(string.Empty);
            }
        }
        [HttpPost]
        public JsonResult GetDetailedLogError(DateTime logErrorDate, string rowKey)
        {
            CloudStorageAccount storageAccount =
                  CloudStorageAccount.Parse("DefaultEndpointsProtocol=" + Constants.Protocol + ";AccountName=" + Constants.AccountName + ";AccountKey=" + Constants.AccountKey);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("Log" + logErrorDate.ToString("ddMMyyyy"));
            if (table.Exists())
            {
                var logEventEntityQuerys = (from entry in table.CreateQuery<LogEventEntityModels>()
                                            where entry.RowKey == rowKey
                                            select entry).ToList();
                return Json(logEventEntityQuerys, JsonRequestBehavior.AllowGet);
            }
            return Json(string.Empty);
        }


    }
}
