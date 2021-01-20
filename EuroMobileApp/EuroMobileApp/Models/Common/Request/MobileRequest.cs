using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models.Common.Request
{
    public class MobileRequest
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public List<int> Ids { get; set; }
    }
}
