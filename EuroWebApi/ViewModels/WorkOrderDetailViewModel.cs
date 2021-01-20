using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class WorkOrderDetailViewModel
    {
        public long WorkOrderId { get; set; }
        public Nullable<int> UserId { get; set; }
        public string CustomerName { get; set; }
        public string ServiceDate { get; set; }
        public string ServiceTime { get; set; }
        public string JobNature { get; set; }
        public string UserFullName { get; set; }
        public string JobStatus { get; set; }
        public string TicketNumber { get; set; }
        public string COD_WARN { get; set; }
        public decimal Mileage { get; set; }
        public string Notes { get; set; }
        public Nullable<int> JobStatusId { get; set; }
        public Nullable<int> JobNatureId { get; set; }
    }
}