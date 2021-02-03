using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class WorkOrderImage
    {
        public long WorkOrderImageId { get; set; }
        public long WorkOrderId { get; set; }
        public string FileName { get; set; }
        public DateTime DateUploaded { get; set; }
        public string Notes { get; set; }
        public bool IsMobileUpload { get; set; }
        public string ImageFullPath { get; set; }
    }
}
