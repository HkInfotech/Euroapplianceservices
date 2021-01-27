using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class WorkOrderServiceModel
    {
        public long ServiceItemid { get; set; }
        public string ServiceItem { get; set; }
        public string ServiceItemDescr { get; set; }
        public decimal ServiceItemPrice { get; set; }
        
    }
}
