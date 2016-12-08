using LoadTestResult.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    public class AzureStorageController : Controller
    {
        //
        // GET: /AzureStorage/

        public JsonResult Index()
        {
           
            try
            {
                CloudStorageAccount storageAccount =
                   CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=logtabletest;AccountKey=Jz3E/bNK7NH9w45PC4vbr6Z1cc0nIjO9J+GBLsg0xz27wwXZtExKOJkdSsXPe3gRoFDvMww5faiPiCdLlk7qmA==");
                CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

                CloudTable table = tableClient.GetTableReference("Log" + DateTime.UtcNow.ToString("ddMMyyyy"));
                table.CreateIfNotExists();

                LogEventEntity logEvent = new LogEventEntity();
                logEvent.Identity = "Walter@contoso.com";
                logEvent.ThreadName = "425-555-0101";
                logEvent.LoggerName = "425-555-0101";
                logEvent.Level = "425-555-0101";
                logEvent.Message = "Find me";
                logEvent.Domain = "425-555-0101";

                // Create the TableOperation that inserts the customer entity.
                var insertOperation = TableOperation.Insert(logEvent);

                // Execute the insert operation.
                table.Execute(insertOperation);

                // Read storage
                //TableQuery<LogEventEntity> query =
                //   new TableQuery<LogEventEntity>()
                //      .Where(TableQuery.GenerateFilterCondition("PartitionKey",
                //          QueryComparisons.Equal, "Harp"));
                //var list = table.ExecuteQuery(query).ToList();
                var logEventEntityQuerys = (from entry in table.CreateQuery<LogEventEntity>()
                                            select entry).ToList();
                logEventEntityQuerys = logEventEntityQuerys.OrderByDescending(e => e.Timestamp).Take(10).ToList();
                return Json(logEventEntityQuerys, JsonRequestBehavior.AllowGet);
                foreach (var logEventEntityQuery in logEventEntityQuerys)
                {
                    //logEventEntityQuery.Timestamp.LocalDateTime
                }

            }
            catch (StorageException ex)
            {
                // Exception handling here.
            }
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public class LogEventEntity : TableEntity
        {
            public LogEventEntity()
            {
                var now = DateTime.UtcNow;
                PartitionKey = string.Format("{0:yyyy-MM}", now);
                RowKey = string.Format("{0:dd HH:mm:ss.fff}-{1}",
                                        now, Guid.NewGuid());
            }

            public string Identity { get; set; }
            public string ThreadName { get; set; }
            public string LoggerName { get; set; }
            public string Level { get; set; }
            public string Message { get; set; }
            public string Domain { get; set; }
        }

    }
}
