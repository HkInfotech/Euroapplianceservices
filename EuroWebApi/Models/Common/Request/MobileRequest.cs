using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common.Request
{
    public class MobileRequest
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public int Id { get; set; }
        public List<int> Ids { get; set; }
        //public DateTime LastSyncDateTime { get; set; }
    }
}