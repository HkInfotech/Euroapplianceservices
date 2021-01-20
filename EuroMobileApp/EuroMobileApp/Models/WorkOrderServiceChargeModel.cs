using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class WorkOrderServiceChargeModel
    {
        public string ServiceTitle { get; set; }
        public decimal ServiceChargeAmount { get; set; }
        public string ServiceDescription { get; set; }
    }
}
