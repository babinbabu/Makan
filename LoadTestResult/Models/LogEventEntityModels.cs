using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoadTestResult.Models
{
    public class LogEventEntityModels: TableEntity
        {
        public LogEventEntityModels()
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