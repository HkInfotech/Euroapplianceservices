﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common.Request
{
    public class WorkOrderFilterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long WorkOrderId { get; set; }
        public long UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
}