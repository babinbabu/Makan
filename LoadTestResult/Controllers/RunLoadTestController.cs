using LoadTestResult.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    public class RunLoadTestController : Controller
    {
        //
        // GET: /RunLoadTest/
        public JsonResult GetLoadTestNames()
        {
            string[] loadtestFiles = System.IO.Directory.GetFiles(Constants.LoadTest_Folder, "*.loadtest");
            List<LoadTestNameModels> TestNameList = new List<LoadTestNameModels>();

            foreach (var loadtestFile in loadtestFiles)
            {
                TestNameList.Add(new LoadTestNameModels(Path.GetFileNameWithoutExtension(loadtestFile), loadtestFile));
            }

            return Json(TestNameList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public bool ExecuteLoadTest(string loadTestPath)
        {
            if (!string.IsNullOrEmpty(loadTestPath) && System.IO.File.Exists(loadTestPath))
            {
                string file = Path.GetFileNameWithoutExtension(loadTestPath) + "-" + DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss") + Constants.File_Extention;
                try
                {
                    Process loadTestProcess = new Process();
                    ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(Constants.MsTest_Url, "/testcontainer:" + loadTestPath + " /resultsfile:" + Constants.LoadTest_Result_Folder + "\\" + file);
                    myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    loadTestProcess.StartInfo = myProcessStartInfo;
                    loadTestProcess.Start();
                    loadTestProcess.WaitForExit();
                    

                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                    return false;
                }
                return true;
            }
            return false;

        }


    }
}
