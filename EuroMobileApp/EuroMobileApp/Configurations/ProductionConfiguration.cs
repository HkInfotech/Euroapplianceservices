using EuroMobileApp.Configurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Configurations
{
    public class ProductionConfiguration : IAppConfiguration
    {
        public string BaseUrl => "http://104.167.11.179";


        public string LocalDB => "";
    }
}
