using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class WorkOrderServiceViewModel
    {
        public Nullable<long> ServiceItemid { get; set; }
        public string ServiceItem { get; set; }
        public string ServiceItemDescr { get; set; }
        public Nullable<decimal> ServiceItemPrice { get; set; }
    }
}