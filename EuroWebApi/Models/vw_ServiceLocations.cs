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
    
    public partial class vw_ServiceLocations
    {
        public long ServiceLocationId { get; set; }
        public long WorkOrderId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public Nullable<int> ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public string InersectionName { get; set; }
        public Nullable<int> ServiceLocaton_Province { get; set; }
        public string ContactName { get; set; }
        public string Province { get; set; }
    }
}
