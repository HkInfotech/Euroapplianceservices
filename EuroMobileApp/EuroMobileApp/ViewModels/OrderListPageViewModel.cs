using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.ViewModels.Base;
using EuroMobileApp.ViewModels.CellViewModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels
{
    public class OrderListPageViewModel : CollectionViewModelBase<OrderCellViewModel>
    {
        public bool IsExpand { get; set; }
        public WorkOrderFilterRequest FilterRequest { get; set; }
        private readonly IAppSettings _appsettings;
        private List<CollectionViewModelBase<OrderCellViewModel>> OrderList;
        public OrderListPageViewModel(IAggregatedServices aggregatedServices, INavigationService navigationService, IAppSettings settings) : base(aggregatedServices)
        {
            OrderList = new List<CollectionViewModelBase<OrderCellViewModel>>();
            _appsettings = settings;
            NavigationService = navigationService;
            IsExpand = false;
            FilterRequest = new WorkOrderFilterRequest();
        }

        private DelegateCommand _logOutCommand;

        public DelegateCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new DelegateCommand(async () =>
        {

            bool confirm = await UserDialogsService.ConfirmAsync(StringResources.LogOutConfirmAlert, null, StringResources.OK, StringResources.Cancel);
            if (confirm)
            {
                _appsettings.ResetDetails();
                await NavigationServiceExtensions.TryNavigateAsync(NavigationService, $"/{PageName.Navigation}/{PageName.LoginPage}", null);
            }

        }));
        private DelegateCommand _navigateToWorkOrderFilterCommand;

        public DelegateCommand NavigateToWorkOrderFilterCommand => _navigateToWorkOrderFilterCommand ?? (_navigateToWorkOrderFilterCommand = new DelegateCommand(async () =>
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("FilterRequest", FilterRequest);
            await NavigationServiceExtensions.TryNavigateAsync(NavigationService, PageName.WorkOrderSearchFilterPage, navigationParameters);

        }));
        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            IsRefreshing = true;
        }
        public override void OnDisappearing()
        {
            base.OnDisappearing();
            IsRefreshing = false;
        }

        async public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            switch (parameters.GetNavigationMode())
            {
                case NavigationMode.Back:
                    {
                        if (parameters.TryGetValue("FilterRequest", out WorkOrderFilterRequest request))
                        {
                            FilterRequest = request;
                            IsRefreshing = true;
                            await RefreshAsync();
                        }
                        break;
                    }
            }


        }

        private async Task GetUserOrders()
        {
            if (IsConnected)
            {
                var List = new List<OrderCellViewModel>();
                IsRefreshing = true;
                var OrderList = await euroMobileService.GetOrdersByUserId();
                if (OrderList?.Count > 0)
                {
                    List.AddRange(OrderList.Select(t => new OrderCellViewModel(t)));
                }
                ListItems = new ObservableCollection<OrderCellViewModel>(List);
                IsRefreshing = false;
                IsEmpty = OrderList?.Count == 0;
            }
            else
            {
                await UserDialogsService.AlertAsync($"{StringResources.OfflineNotAvailableForResource}", null, StringResources.OK);
            }
        }
        private async Task GetUserOrderByFilter()
        {
            if (IsConnected)
            {
                var List = new List<OrderCellViewModel>();
                IsRefreshing = true;
                FilterRequest.UserId = Convert.ToInt64(_appsettings.UserId);
                if (!string.IsNullOrEmpty(FilterRequest.PhoneNumber))
                {
                    string RemovePhoneNumberMask = Regex.Replace(FilterRequest.PhoneNumber, @"[^\d]", "");
                    FilterRequest.PhoneNumber = RemovePhoneNumberMask;
                }
               
               
                var OrderList = await euroMobileService.GetOrdersByFilter(FilterRequest);
                if (OrderList?.Count > 0)
                {
                    List.AddRange(OrderList.Select(t => new OrderCellViewModel(t)));
                }
                ListItems = new ObservableCollection<OrderCellViewModel>(List);
                IsRefreshing = false;
                IsEmpty = OrderList?.Count == 0;
            }
            else
            {
                await UserDialogsService.AlertAsync($"{StringResources.OfflineNotAvailableForResource}", null, StringResources.OK);
            }
        }
        async protected override Task OnItemSelectedAsync(OrderCellViewModel item)
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("OrderModel", item.Order);
            await NavigationServiceExtensions.TryNavigateAsync(NavigationService, $"{PageName.WorkOrderDetailPage}", navigationParameters);
        }
        protected override async Task RefreshAsync()
        {

            await GetUserOrderByFilter();
            IsRefreshing = false;
        }

    }
}
