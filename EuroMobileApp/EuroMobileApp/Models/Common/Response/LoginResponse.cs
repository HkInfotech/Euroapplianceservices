﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Response
{
    public class LoginResponse
    {
        public long UserId { get; set; }
        public string LoginName { get; set; }
        public string UserFullName { get; set; }
        public string UserPassword { get; set; }
        public int UserTypeId { get; set; }
        public bool Inactive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string status { get; set; }
    }
}
