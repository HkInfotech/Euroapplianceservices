using EuroMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EuroMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersListPage : ContentPage
    {
        public OrdersListPage()
        {
            InitializeComponent();

            BindingContext = new OrdersListVM();
        }

        protected override void OnAppearing()
        {
            // Inform the view model that it is appearing
            var viewModel = BindingContext as OrdersListVM;

            // Inform the view model that it is appearing.
            viewModel?.OnAppearing();
        }
    }
}