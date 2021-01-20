using EuroMobileApp.Helpers;
using EuroMobileApp.Models.Common.Request;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels
{
    public class WorkOrderSearchFilterPageViewModel : ViewModelBase
    {
        public WorkOrderFilterRequest request { get; set; }

       
        public WorkOrderSearchFilterPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            request = new WorkOrderFilterRequest();
        }
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("FilterRequest", out WorkOrderFilterRequest filterRequest))
            {
                request = filterRequest;
            }
        }
        private DelegateCommand _navigateToWorkOrderListCommand;

        public DelegateCommand NavigateToWorkOrderListCommand => _navigateToWorkOrderListCommand ?? (_navigateToWorkOrderListCommand = new DelegateCommand(async () =>
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("FilterRequest", request);
            await NavigationService.GoBackAsync(navigationParameters);

        }));
        
    }
}
