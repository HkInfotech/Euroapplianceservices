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
    
    public partial class SP_getWorkOrder_Payments_Result
    {
        public long PaymentId { get; set; }
        public long WorkOrderLineId { get; set; }
        public Nullable<long> WorkOrderId { get; set; }
        public string LineType { get; set; }
        public string ServiceName { get; set; }
        public Nullable<decimal> ServiceCharges { get; set; }
        public Nullable<decimal> Tax { get; set; }
        public decimal PaymentRcvd { get; set; }
        public Nullable<decimal> BalancePayment { get; set; }
        public int PaymentModeId { get; set; }
        public string PaymentMode { get; set; }
    }
}
