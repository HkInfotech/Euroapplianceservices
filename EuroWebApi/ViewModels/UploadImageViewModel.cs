using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class UploadImageViewModel
    {
        public byte[] image { get; set; }
        public string imgName { get; set; }
        public int workOrderId { get; set; }
        public int userId { get; set; }
        public string customerName { get; set; }
        public string notes { get; set; }
    }
}