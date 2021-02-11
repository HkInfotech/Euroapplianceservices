using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace EuroWebApi.Utils
{

    public static class ErrorLog
    {
        public static void WriteError(Exception ex, string moduleName)
        {
            var line = Environment.NewLine + Environment.NewLine;
            var ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
            var Errormsg = ex.GetType().Name.ToString();
            var extype = ex.GetType().ToString();
            //var exurl = context.Current.Request.Url.ToString();
            var ErrorLocation = ex.Message.ToString();
            try
            {
                string filepath = System.Web.Hosting.HostingEnvironment.MapPath("~/ErrorLog/"); //Text File Path

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                if (!File.Exists(filepath))
                {
                    File.Create(filepath).Dispose();
                }

                using (StreamWriter sw = File.AppendText(filepath))
                {
                    string error
                        = "Log Written Date: " + DateTime.Now.ToString() + line
                        + "Module Name : " + moduleName + line
                        + "Error Line No : " + ErrorlineNo + line
                        + "Error Message: " + Errormsg + line
                        + "Error Type: " + extype + line
                        + "Error Location: " + ErrorLocation + line
                        + "Stack Trace: " + ex.StackTrace + line
                        + (ex.InnerException != null ? "Inner Exception Message: " + ex.InnerException.Message + line : "")
                        + (ex.InnerException != null && ex.InnerException.StackTrace != null ? "Inner Exception Stack Trace : " + ex.InnerException.StackTrace + line : "");

                    sw.WriteLine("---------- *Exception Details on " + " " + DateTime.Now.ToString() + "* ----------");
                    sw.WriteLine("----------------------------------------------------------------------------------");
                    sw.WriteLine(error);
                    sw.WriteLine("-------------------------------- *End* -------------------------------------------");
                    sw.WriteLine(line);
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }

        }
    }
}