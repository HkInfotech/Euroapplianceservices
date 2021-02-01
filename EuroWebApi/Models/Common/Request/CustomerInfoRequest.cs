using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common.Request
{
    public class CustomerInfoRequest
    {
        public long CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string AddressLine { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string NearestIntersection { get; set; }
        public string PhoneNumber { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}