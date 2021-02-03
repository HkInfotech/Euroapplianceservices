using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class WorkOrderPartViewModel
    {
        public string PartName { get; set; }
        public long WorkOrderPartID { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> InvoicePrice { get; set; }
        public string Notes { get; set; }
        public long WorkOrderId { get; set; }
        public int  UserId { get; set; }
    }


    public class WorkOrderViewModel
    {
        public long CustomerId { get; set; }
        public long WorkOrderId { get; set; }
    }
    public class CustomerViewModel
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}