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
    
    public partial class WorkOrder_Schedule
    {
        public long WorkOrderScheduleId { get; set; }
        public long WorkOrderId { get; set; }
        public System.DateTime ServiceDate { get; set; }
        public int ServiceTime { get; set; }
    }
}
