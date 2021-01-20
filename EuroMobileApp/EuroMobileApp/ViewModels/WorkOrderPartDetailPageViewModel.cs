using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.Validations;
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
    public class WorkOrderPartDetailPageViewModel : ViewModelBase
    {
        #region Properties
        public WorkOrderPartModel RequestWorkOrderPartModel { get; set; }
        #endregion
        #region Services
        private readonly IEuroMobileService _euroMobileService;
        private readonly IAppSettings _appSettings;
        #endregion

        #region Command
        private DelegateCommand _saveCommand;
        public DelegateCommand SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand(async () => await ExecuteSaveCommand()));

        async Task ExecuteSaveCommand()
        {
            ValidateFields();
            try
            {
                if (IsConnected == true)
                {
                    if (PartName.IsValid && PartQuentity.IsValid && PartRate.IsValid && TotalAmount.IsValid)
                    {
                        IsBusy = true;
                        RequestWorkOrderPartModel.PartName = _partName.Value;
                        RequestWorkOrderPartModel.Quantity = Convert.ToDecimal(_partQuentity.Value);
                        RequestWorkOrderPartModel.Rate = Convert.ToDecimal(_partRate.Value);
                        RequestWorkOrderPartModel.PurchasePrice = Convert.ToDecimal(TotalAmount.Value);
                        RequestWorkOrderPartModel.Notes = PartNote;
                        RequestWorkOrderPartModel.UserId = Convert.ToInt32(_appSettings.UserId);
                        var result = await _euroMobileService.SaveWorkOrderPart(RequestWorkOrderPartModel);
                        NavigationParameters navigationParameters = new NavigationParameters()
                        {
                            {
                                "PartModel",RequestWorkOrderPartModel
                            },
                            {
                                "SaveWorkOrderPartResponse",result
                            }
                        };
                        IsBusy = false;
                        await NavigationService.GoBackAsync(navigationParameters);
                    }
                }
                else
                {
                    await UserDialogsService.AlertAsync($"{StringResources.OfflineNotAvailableForResource}", null, StringResources.OK);
                }

            }
            catch (Exception ex)
            {

            }

        }
        #endregion
        #region Properties and it's Command

        private ValidatableObject<string> _partName = new ValidatableObject<string>() { Value = string.Empty };

        public ValidatableObject<string> PartName
        {
            get { return _partName; }
            set
            {
                _partName = value;
                RaisePropertyChanged(() => PartName);
            }
        }

        private DelegateCommand _validatePartNameCommand;

        public DelegateCommand ValidatePartNameCommand =>
            _validatePartNameCommand ?? (_validatePartNameCommand = new DelegateCommand(async () => await ExecuteValidatePartNameCommandAsync()));

        private async Task<bool> ExecuteValidatePartNameCommandAsync()
        {
            return _partName.Validate();
        }

        private ValidatableObject<decimal> _partQuentity = new ValidatableObject<decimal>() { Value = 0 };

        public ValidatableObject<decimal> PartQuentity
        {
            get { return _partQuentity; }
            set
            {
                _partQuentity = value;
                RaisePropertyChanged(() => PartQuentity);
            }
        }

        private DelegateCommand _validatePartQuentityCommand;

        public DelegateCommand ValidatePartQuentityCommand =>
            _validatePartQuentityCommand ?? (_validatePartQuentityCommand = new DelegateCommand(async () => await ExecuteValidatePartQuentityCommandAsync()));

        private async Task<bool> ExecuteValidatePartQuentityCommandAsync()
        {
            await CalculateTotalAmount();
            return _partQuentity.Validate();
        }
        private async Task CalculateTotalAmount()
        {
            decimal Qty = 0;
            decimal Rate = 0;
            if (_partQuentity.Value != 0 && _partRate.Value != 0)
            {
                Qty = PartQuentity.Value;
                Rate = PartRate.Value;
                TotalAmount.Value = Qty * Rate;
            }
            else
            {
                TotalAmount.Value = 0;
            }
        }
        private ValidatableObject<decimal> _partRate = new ValidatableObject<decimal>() { Value = 0 };

        public ValidatableObject<decimal> PartRate
        {
            get { return _partRate; }
            set
            {
                _partRate = value;
                RaisePropertyChanged(() => PartRate);
            }
        }

        private DelegateCommand _validatePartRateCommand;

        public DelegateCommand ValidatePartRateCommand =>
            _validatePartRateCommand ?? (_validatePartRateCommand = new DelegateCommand(async () => await ExecuteValidatePartRateCommandAsync()));

        private async Task<bool> ExecuteValidatePartRateCommandAsync()
        {
            await CalculateTotalAmount();
            return _partRate.Validate();
        }

        private ValidatableObject<decimal> _totalAmount = new ValidatableObject<decimal>() { Value = 0 };

        public ValidatableObject<decimal> TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                _totalAmount = value;
                RaisePropertyChanged(() => TotalAmount);
            }
        }

        private DelegateCommand _validateTotalAmountCommand;

        public DelegateCommand ValidateTotalAmountCommand =>
            _validateTotalAmountCommand ?? (_validateTotalAmountCommand = new DelegateCommand(async () => await ExecuteValidateTotalAmountCommandAsync()));

        private async Task<bool> ExecuteValidateTotalAmountCommandAsync()
        {
            return _totalAmount.Validate();
        }

        public string PartNote { get; set; }

        #endregion
        #region Constructor
        public WorkOrderPartDetailPageViewModel(INavigationService navigationService, IEuroMobileService euroMobileService, IAppSettings settings) : base(navigationService)
        {
            PartName = new ValidatableObject<string>() { Value = string.Empty };
            PartQuentity = new ValidatableObject<decimal>() { Value = 0 };
            PartRate = new ValidatableObject<decimal>() { Value = 0 };
            PartNote = "";
            TotalAmount = new ValidatableObject<decimal>() { Value = 0 };
            RequestWorkOrderPartModel = new WorkOrderPartModel();
            AddValidations();
            _euroMobileService = euroMobileService;
            _appSettings = settings;
        }
        #endregion

        #region Methods
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.TryGetValue("PartModel", out WorkOrderPartModel partmodel))
            {
                RequestWorkOrderPartModel = partmodel;
                PartName.Value = RequestWorkOrderPartModel.PartName;
                PartQuentity.Value = RequestWorkOrderPartModel.Quantity;
                PartRate.Value = RequestWorkOrderPartModel.Rate ?? 0;
                PartNote = RequestWorkOrderPartModel.Notes;
                TotalAmount.Value = RequestWorkOrderPartModel.PurchasePrice ?? 0;
            }
        }

        protected void ValidateFields()
        {
            ValidatePartNameCommand.Execute();
            ValidatePartQuentityCommand.Execute();
            ValidatePartRateCommand.Execute();
            ValidateTotalAmountCommand.Execute();
        }

        protected void AddValidations()
        {
            _partName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.PartNameRequired
            });
            _partQuentity.Validations.Add(new ValueIsNotZeroRules<decimal>
            {
                ValidationMessage = StringResources.QuantityRequired
            });
            _partRate.Validations.Add(new ValueIsNotZeroRules<decimal>
            {
                ValidationMessage = StringResources.RateRequired
            });
            _totalAmount.Validations.Add(new ValueIsNotZeroRules<decimal>
            {
                ValidationMessage = StringResources.TotalAmountRequired
            });
        }
        #endregion


    }
}
