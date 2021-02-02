using EuroMobileApp.Models;
using EuroMobileApp.Services.Interfaces;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels.SignaturePageContententViewModels
{
    public class OrderTotalViewModel : ViewModelBase
    {

        #region Services
        private readonly IEuroMobileService _euroServices;
        #endregion

        #region Properties
        private InvoiceTotalModel _totalOrder;

        public InvoiceTotalModel TotalOrder
        {
            get { return _totalOrder; }
            set { SetProperty(ref _totalOrder, value); }
        }
        #endregion

        #region Variable

        #endregion

        #region Command


        #endregion

        #region Constructor
        public OrderTotalViewModel(INavigationService navigationService, IEuroMobileService _euroServices) : base(navigationService)
        {
            TotalOrder = new InvoiceTotalModel();
            this._euroServices = _euroServices;
        }

        #endregion

        #region Methods
        public async Task GetInvoiceTotal()
        {
            if (IsConnected)
            {

                TotalOrder = await _euroServices.GetInvoiceTotal(21668);

            }
        }

        async public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await GetInvoiceTotal();
        }
        #endregion

    }
}
