using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.ModelDTO
{
    public class ApplianceDTO
    {
        public int WorkorderId { get; set; }
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerApplianceId { get; set; }
        public int ApplianceTypeId { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public string ModelImage { get; set; }

        public string ApplianceName { get; set; }
    }
}