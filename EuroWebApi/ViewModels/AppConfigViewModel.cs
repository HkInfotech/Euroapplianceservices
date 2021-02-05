using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.ViewModels
{
    public class AppConfigViewModel
    {
        public long AppConfigId { get; set; }
        public string ParentKey { get; set; }
        public string ChildKEy { get; set; }
        public string ConfigValue { get; set; }
    }
}