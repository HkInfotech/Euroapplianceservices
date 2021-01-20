using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Configurations
{
    public interface IAppConfiguration
    {
        string BaseUrl { get; }

        string LocalDB { get; }
    }
}
