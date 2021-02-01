using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Request
{
    public class CustomerRequest
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool Inactive { get; set; }
        public bool CustomerStatus { get; set; }
    }
}
