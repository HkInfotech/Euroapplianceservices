using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class ApplianceViewModel
    {
        public long WorkorderId { get; set; }
        public long UserId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long CustomerApplianceId { get; set; }
        public long ApplianceTypeId { get; set; }
        public long ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string SerialNumber { get; set; }
        public string ModelNumber { get; set; }
        public string ModelImage { get; set; }

        public string ApplianceName { get; set; }
        public string ImageFile1 { get; set; }
        public string ImageFile2 { get; set; }
        public string ImageFile3 { get; set; }
        public string ImageFile4 { get; set; }
        public List<DocumentViewModel> ApplianceDocuments { get; set; }

    }
}