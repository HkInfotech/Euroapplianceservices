using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string Version { get; set; }
        public DateTime RequestDateTime { get; set; }
        public double TotalResponseTimeMS
        {
            get
            {
                ResponseStopWatch.Stop();
                TimeSpan ts = ResponseStopWatch.Elapsed;
                return ts.TotalMilliseconds;
            }
        }
        public T ResponseContent { get; set; }

        private Stopwatch ResponseStopWatch = new Stopwatch();

        public Response()
        {
            ResponseStopWatch.Start();
            Version = "2.0";
            RequestDateTime = DateTime.Now;
        }

        public void Fail()
        {
            Success = false;
            Message = "Failure!";
            StackTrace = "";
        }

        public void Fail(string message)
        {
            Success = false;
            Message = message;
            StackTrace = "";
        }

        public void Fail(string message, string stackTrace)
        {
            Success = false;
            Message = message;
            StackTrace = stackTrace;
        }
    }
}