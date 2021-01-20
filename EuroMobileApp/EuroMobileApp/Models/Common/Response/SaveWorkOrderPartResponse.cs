using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Response
{
    public class SaveWorkOrderPartResponse
    {
        public string Status { get; set; }
        public Nullable<long> WorkOrderPartId { get; set; }
        public Nullable<long> UserId { get; set; }
        public Nullable<long> WorKorderId { get; set; }
    }
}
