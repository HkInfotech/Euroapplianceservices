using EuroMobileApp.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Configurations
{
    public class DevConfiguration : IAppConfiguration
    {
        public string BaseUrl => "http://192.168.0.105:61177";
        public string LocalDB => "";
    }
}
