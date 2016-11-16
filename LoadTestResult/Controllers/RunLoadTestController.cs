using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoadTestResult.Controllers
{
    public class RunLoadTestController : Controller
    {
        //
        // GET: /RunLoadTest/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RunTest(string testPath = "D:\\project\\JxTest\\JxLoadTest\\JxLoadTest\\Sequential.loadtest")
        {
            if (!string.IsNullOrEmpty(testPath) && System.IO.File.Exists(testPath))
            {
                string file = DateTime.UtcNow.ToString("yyyyMMddHHmmss") + Constants.File_Extention;
                try
                {
                    Process loadTestProcess = new Process();
                    ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(Constants.MsTest_Url, "/testcontainer:" + testPath + " /resultsfile:" + Constants.LoadTest_Result_Folder + "\\" + file);

                    myProcessStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    loadTestProcess.StartInfo = myProcessStartInfo;
                    loadTestProcess.Start();
                    loadTestProcess.WaitForExit();
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            return View();

        }

    }
}
