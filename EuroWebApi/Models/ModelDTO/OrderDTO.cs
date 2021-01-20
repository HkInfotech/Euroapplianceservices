using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.ModelDTO
{
    public class OrderDTO
    {
        public long UserId { get; set; }
        public long WorkOrderId { get; set; }
        public DateTime WorkOrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ApplianceType { get; set; }
        public string JobStatus { get; set; }
        public string message { get; set; }
    }
}