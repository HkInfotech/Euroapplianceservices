//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EuroWebApi.Models
{
    using System;
    
    public partial class SP_WorkOrderParts_Result
    {
        public Nullable<long> WorkOrderId { get; set; }
        public Nullable<System.DateTime> WorkOrderDate { get; set; }
        public string ServiceDates { get; set; }
        public string CustomerName { get; set; }
        public string PartName { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal InvoicePrice { get; set; }
        public Nullable<decimal> Diff { get; set; }
    }
}
