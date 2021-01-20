using EuroMobileApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Services.Implements
{
    public class AggregatedServices : IAggregatedServices
    {
        public IAccountService accountService { get; private set; }
        public IEuroMobileService euroMobileService { get; private set; }

        public AggregatedServices(IAccountService accountService,IEuroMobileService euroMobileService)
        {
            this.accountService = accountService;
            this.euroMobileService = euroMobileService;
        }
    }
}
