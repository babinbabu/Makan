using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LoadTestResult
{
    public class Constants
    {
        public static readonly string Base_Folder_Url = Path.GetFullPath(Path.Combine(HttpRuntime.AppDomainAppPath, @"..\"));
        public static readonly string MsTest_Url = System.Configuration.ConfigurationManager.AppSettings["MsTest"];
        public static readonly string LoadTest_Folder_Name = System.Configuration.ConfigurationManager.AppSettings["LoadTestFolderName"];
        public static readonly string LoadTest_Result_Folder_Name = System.Configuration.ConfigurationManager.AppSettings["LoadTestResultFolderName"];
        public static readonly string LoadTest_Folder = string.Format("{0}{1}", Constants.Base_Folder_Url, Constants.LoadTest_Folder_Name);
        public static readonly string LoadTest_Result_Folder = string.Format("{0}{1}", Constants.Base_Folder_Url, Constants.LoadTest_Result_Folder_Name);
        public static readonly string File_Extention = ".trx";
        public static readonly double Malaysia_Time_Zone = 8;
        public static readonly string Range = "1";
        public static readonly string Protocol = System.Configuration.ConfigurationManager.AppSettings["Protocol"];
        public static readonly string AccountName = System.Configuration.ConfigurationManager.AppSettings["AccountName"];
        public static readonly string AccountKey = System.Configuration.ConfigurationManager.AppSettings["AccountKey"];

    }
}