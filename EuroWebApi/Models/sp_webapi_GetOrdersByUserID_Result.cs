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
    
    public partial class sp_webapi_GetOrdersByUserID_Result
    {
        public Nullable<long> UserId { get; set; }
        public long WorkOrderId { get; set; }
        public Nullable<System.DateTime> WorkOrderDate { get; set; }
        public string CustomerName { get; set; }
        public string ApplianceType { get; set; }
        public string JobStatus { get; set; }
        public Nullable<long> CustomerId { get; set; }
    }
}
