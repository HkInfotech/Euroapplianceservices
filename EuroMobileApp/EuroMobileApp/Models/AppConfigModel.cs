using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class AppConfigModel
    {
        public long AppConfigId { get; set; }
        public string ParentKey { get; set; }
        public string ChildKEy { get; set; }
        public string ConfigValue { get; set; }
    }
}
