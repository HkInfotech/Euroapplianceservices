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
    using System.Collections.Generic;
    
    public partial class WorkOrder_Services
    {
        public long WorkOrderServId { get; set; }
        public Nullable<long> WorkOrderId { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public Nullable<decimal> ServiceCharges { get; set; }
        public Nullable<decimal> OtherCharges { get; set; }
        public Nullable<decimal> LabourCharges { get; set; }
        public Nullable<decimal> SHCharges { get; set; }
        public string ServiceNotes { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
    }
}
