using EuroMobileApp.Models;
using EuroMobileApp.Views;
using RestSharp;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroMobileApp.ViewModels
{
    public class OrdersListVM : BaseVM
    {
        private bool _isBusy { get; set; }
        public bool isBusy
        {
            get { return _isBusy; }
            set
            {
                if ((!_isBusy) && value)
                    Application.Current.MainPage.Navigation.PushPopupAsync(new PopupLoading());
                else if (_isBusy && (!value))
                    Application.Current.MainPage.Navigation.PopPopupAsync();
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private OrdersListModel _ordersList { get; set; }
        public OrdersListModel ordersList
        {
            get { return _ordersList; }
            set
            {
                _ordersList = value;
                OnPropertyChanged();
            }
        }
        private OrdersListModel originalOrderList { get; set; }

        private string _SearchText { get; set; }
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }
        }

        private clsOrder _SelectedItem { get; set; }
        public clsOrder SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                //
            }
        }

        public OrdersListVM()
        {
            ordersList = new OrdersListModel();
            originalOrderList = new OrdersListModel();
        }

        public virtual void OnAppearing()
        {
            LoadList();
        }

        private async void LoadList()
        {
            isBusy = true;
            int userID = Convert.ToInt32(App.Current.Properties["userid"]);

            var client = new RestClient(GlobalApiMaintain.EuroApiUrl);
            var request = new RestRequest("/EuroApi/GetOrdersByUserId", Method.GET).AddParameter("UserId", userID);
            var response = await client.GetAsync<OrdersListModel>(request);

            if (response.status)
            {
                ordersList = response;
                originalOrderList.data = ordersList.data;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.message, "OK");
            }
            isBusy = false;
        }

        public Command SearchCommand 
        { 
            get
            {
                return new Command(() => {
                    if (string.IsNullOrEmpty(SearchText) || string.IsNullOrWhiteSpace(SearchText))
                        ordersList.data = originalOrderList.data;
                    else
                        ordersList.data = originalOrderList.data.Where(x=>x.WorkOrderId.ToString().Contains(SearchText)).ToList();
                });
            }
        }

        public Command LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Application.Current.Properties.Clear();
                    Application.Current.MainPage = new LoginPage();
                });
            }
        }
    }
}
