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
    
    public partial class vw_Customers
    {
        public long CustomerId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        public string ReferenceBy { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address_Line_1 { get; set; }
        public string Address_Line_2 { get; set; }
        public string City { get; set; }
        public Nullable<int> ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<bool> Inactive { get; set; }
        public Nullable<bool> CustomerStatus { get; set; }
        public string Province { get; set; }
    }
}