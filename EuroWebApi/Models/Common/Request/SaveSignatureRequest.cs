using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common.Request
{
    public class SaveSignatureRequest
    {
        public long WorkOrderId { get; set; }
        public string InvoiceSigned { get; set; }
        public string covidanswer1 { get; set; }
        public string covidanswer2 { get; set; }
        public string covidanswer3 { get; set; }
    }
}