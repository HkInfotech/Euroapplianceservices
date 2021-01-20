using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class WorkOrderPartModel
    {
        public string PartName { get; set; }
        public long WorkOrderPartID { get; set; }
        public decimal Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }
        public Nullable<decimal> PurchasePrice { get; set; }
        public Nullable<decimal> InvoicePrice { get; set; }
        public string Notes { get; set; }
        public long WorkOrderId { get; set; }
        public int UserId { get; set; }

    }
}
