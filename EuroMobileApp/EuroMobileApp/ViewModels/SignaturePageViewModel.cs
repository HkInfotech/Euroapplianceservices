using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels
{

    public class SignaturePageViewModel : ViewModelBase
    {
        #region Services
        private readonly IEuroMobileService _euroServices;
        #endregion

        #region Properties
        public string PositionSelected { get; set; }
        public InvoiceTotalModel InvoiceTotalItem { get; set; }

        public OrderModel SelectedWorkOrderModel { get; set; }
        private InvoiceTotalModel _totalOrder;

        public InvoiceTotalModel TotalOrder
        {
            get { return _totalOrder; }
            set { SetProperty(ref _totalOrder, value); }
        }


        public string LimitedWarrantyInfo { get; set; }
        public string LiabilitiesInfo { get; set; }
        public string SignatureViewTitle { get; set; }
        public bool InvoiceSigned { get; set; }

        public List<Covid19FormModel> covid19FormModels { get; set; }
        public CustomerInvoiceSignatureModel CustomerInvoiceSignature { get; set; }
        #endregion

        #region Variable

        #endregion

        #region Command
        private DelegateCommand<string> _selectItemCommand;

        public DelegateCommand<string> SelectItemCommand =>
            _selectItemCommand ?? (_selectItemCommand = new DelegateCommand<string>(async (a) => await ExecuteSelectItemCommand(a)));

        private async Task ExecuteSelectItemCommand(string parameter)
        {
            PositionSelected = parameter;

        }
        private DelegateCommand _pleaseSigninCommand;

        public DelegateCommand PleaseSigninCommand =>
            _pleaseSigninCommand ?? (_pleaseSigninCommand = new DelegateCommand(async () => await ExecutePleaseSigninCommand()));

        private async Task ExecutePleaseSigninCommand()
        {
            SignatureViewTitle = SelectedWorkOrderModel.CustomerName;
            InvoiceSigned = true;
        }

        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            try
            {
                if (IsConnected)
                {
                    IsBusy = true;
                    SaveSignatureRequest saveSignatureRequest = new SaveSignatureRequest();
                    saveSignatureRequest.covidanswer1 = covid19FormModels[0].QuestionAnswer ? "Y" : "N";
                    saveSignatureRequest.covidanswer2 = covid19FormModels[1].QuestionAnswer ? "Y" : "N";
                    saveSignatureRequest.covidanswer3 = covid19FormModels[2].QuestionAnswer ? "Y" : "N";
                    saveSignatureRequest.InvoiceSigned = InvoiceSigned ? "Y" : "N";
                    saveSignatureRequest.WorkOrderId = SelectedWorkOrderModel.WorkOrderId;
                    var result = await _euroServices.SaveSignature(saveSignatureRequest);
                    await GetInvoiceSignature();
                    IsBusy = false;
                }
            }
            catch (Exception)
            {
                IsBusy = false;
            }

        }
        #endregion

        #region Constructor
        public SignaturePageViewModel(INavigationService navigationService, IEuroMobileService _euroServices) : base(navigationService)
        {
            PositionSelected = "0";
            this._euroServices = _euroServices;
            InvoiceTotalItem = new InvoiceTotalModel();
            TotalOrder = new InvoiceTotalModel();
            SignatureViewTitle = StringResources.OrderTotalCustomerSignature;
            SelectedWorkOrderModel = new OrderModel();
            covid19FormModels = new List<Covid19FormModel>()
            {
                new Covid19FormModel()
                {
                    QuestionTitle=StringResources.CovidQuestion1,
                    QuestionAnswer=false
                },
                new Covid19FormModel()
                {
                    QuestionTitle=StringResources.CovidQuestion2,
                    QuestionAnswer=false
                },
                new Covid19FormModel()
                {
                    QuestionTitle=StringResources.CovidQuestion3,
                    QuestionAnswer=false
                }
            };
            CustomerInvoiceSignature = new CustomerInvoiceSignatureModel();
            InvoiceSigned = false;

        }
        #endregion

        #region Methods
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("SelectedWorkOrderModel", out OrderModel orderModel))
            {
                SelectedWorkOrderModel = orderModel;
            }
            IsBusy = true;
            await GetInvoiceTotal();
            await GetInvoiceText();
            await GetInvoiceSignature();
            
            IsBusy = false;
        }
        public async override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("OrderModel", SelectedWorkOrderModel);
            await NavigationService.GoBackAsync(navigationParameters);

            // await NavigationServiceExtensions.TryNavigateBackAsync(NavigationService, navigationParameters);
        }

        public async Task GetInvoiceTotal()
        {
            if (IsConnected)
            {
                InvoiceTotalItem = new InvoiceTotalModel();
                InvoiceTotalItem = await _euroServices.GetInvoiceTotal(SelectedWorkOrderModel.WorkOrderId);
                TotalOrder = InvoiceTotalItem;
            }
        }

        public async Task GetInvoiceText()
        {
            if (IsConnected)
            {
                InvoiceTotalItem = new InvoiceTotalModel();
                LimitedWarrantyInfo = await _euroServices.GetInvoiceText("W");
                LiabilitiesInfo = await _euroServices.GetInvoiceText("L");
            }

        }
        public async Task GetInvoiceSignature()
        {
            if (IsConnected)
            {
                CustomerInvoiceSignature = new CustomerInvoiceSignatureModel();
                CustomerInvoiceSignature = await _euroServices.GetInvoiceSignatureInfo(SelectedWorkOrderModel.WorkOrderId);
                if (CustomerInvoiceSignature.CustomerSignatureId != 0)
                {
                    covid19FormModels = new List<Covid19FormModel>()
                    {
                        new Covid19FormModel()
                        {
                            QuestionTitle=StringResources.CovidQuestion1,
                            QuestionAnswer=CustomerInvoiceSignature.Covid_answer_1=="Y"||CustomerInvoiceSignature.Covid_answer_1=="y"?true:false
                        },
                        new Covid19FormModel()
                        {
                            QuestionTitle = StringResources.CovidQuestion2,
                            QuestionAnswer=CustomerInvoiceSignature.Covid_answer_2=="Y"||CustomerInvoiceSignature.Covid_answer_2=="y"?true:false
                        },
                        new Covid19FormModel()
                        {
                            QuestionTitle = StringResources.CovidQuestion3,
                            QuestionAnswer=CustomerInvoiceSignature.Covid_answer_3=="Y"||CustomerInvoiceSignature.Covid_answer_3=="y"?true:false
                        }
                    };
                    InvoiceSigned = CustomerInvoiceSignature.InvoiceSigned == "Y" || CustomerInvoiceSignature.InvoiceSigned == "y" ? true : false;
                    SignatureViewTitle = InvoiceSigned ? SelectedWorkOrderModel.CustomerName : StringResources.OrderTotalCustomerSignature;
                }
            }

        }

        private async Task GetWorkOrderDetailTabInfo()
        {
            try
            {
                // Detail Data Fill
                var details = await _euroServices.GetWorkOrderDetailById(SelectedWorkOrderModel.WorkOrderId);
            }
            catch
            {

            }
        }

        public async override Task ExecuteNavigateGoBackCommand()
        {

            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("OrderModel", SelectedWorkOrderModel);
            await NavigationService.GoBackAsync(navigationParameters);
            //await NavigationServiceExtensions.TryNavigateBackAsync(NavigationService, navigationParameters);

        }
        #endregion

    }
}
