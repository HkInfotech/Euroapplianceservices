using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Services.Interfaces
{
    public interface IAggregatedServices
    {
        IAccountService accountService { get; }
        IEuroMobileService euroMobileService { get; }

    }
}
