using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class clsOrder
    {
        public int UserId { get; set; }
        public int WorkOrderId { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ApplianceType { get; set; }
        public string JobStatus { get; set; }
        public string message { get; set; }

        public string GetWorkOrderDate { 
            get { return WorkOrderDate.ToShortDateString(); }
        }

    }
}
