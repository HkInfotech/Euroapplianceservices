using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Version { get; set; }
        public string StackTrace { get; set; }
        public string RequestDateTime { get; set; }
        public T ResponseContent { get; set; }
        public decimal TotalResponseTimeMS { get; set; }
    }
}
