using EuroMobileApp.Helpers;
using EuroMobileApp.Validations;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EuroMobileApp.ViewModels
{
    public class CustomerDetailPageViewModel : ViewModelBase
    {

        #region Services

        #endregion

        #region Properties

        #endregion

        #region Variable

        #endregion

        #region Command


        #endregion


        #region Properties and it's Commands
        // First Name
        private ValidatableObject<string> _customerFirstName = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerFirstName };

        public ValidatableObject<string> CustomerFirstName
        {
            get { return _customerFirstName; }
            set
            {
                _customerFirstName = value;
                RaisePropertyChanged(() => CustomerFirstName);
            }
        }

        private DelegateCommand _validateCustomerFirstNameCommand;

        public DelegateCommand ValidateCustomerFirstNameCommand =>
            _validateCustomerFirstNameCommand ?? (_validateCustomerFirstNameCommand = new DelegateCommand(() => ExecuteValidateCustomerFirstNameCommand()));

        private bool ExecuteValidateCustomerFirstNameCommand()
        {
            return _customerFirstName.Validate();
        }

        // Customer Last name
        private ValidatableObject<string> _customerLastName = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerLastName };

        public ValidatableObject<string> CustomerLastName
        {
            get { return _customerLastName; }
            set
            {
                _customerLastName = value;
                RaisePropertyChanged(() => CustomerLastName);
            }
        }

        private DelegateCommand _validateCustomerLastNameCommand;

        public DelegateCommand ValidateCustomerLastNameCommand =>
            _validateCustomerLastNameCommand ?? (_validateCustomerLastNameCommand = new DelegateCommand(() => ExecuteValidateCustomerLastNameCommand()));

        private bool ExecuteValidateCustomerLastNameCommand()
        {
            return _customerLastName.Validate();
        }

        //Customer address
        private ValidatableObject<string> _customerAddress = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerAddress };

        public ValidatableObject<string> CustomerAddress
        {
            get { return _customerAddress; }
            set
            {
                _customerAddress = value;
                RaisePropertyChanged(() => CustomerAddress);
            }
        }

        private DelegateCommand _validateCustomerAddressCommand;

        public DelegateCommand ValidateCustomerAddressCommand =>
            _validateCustomerAddressCommand ?? (_validateCustomerAddressCommand = new DelegateCommand(() => ExecuteValidateCustomerAddressCommand()));

        private bool ExecuteValidateCustomerAddressCommand()
        {
            return _customerAddress.Validate();
        }

        // Customer City
        private ValidatableObject<string> _customerCity = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerCity };

        public ValidatableObject<string> CustomerCity
        {
            get { return _customerCity; }
            set
            {
                _customerCity = value;
                RaisePropertyChanged(() => CustomerCity);
            }
        }

        private DelegateCommand _validateCustomerCityCommand;

        public DelegateCommand ValidateCustomerCityCommand =>
            _validateCustomerCityCommand ?? (_validateCustomerCityCommand = new DelegateCommand(() => ExecuteValidateCustomerCityCommand()));

        private bool ExecuteValidateCustomerCityCommand()
        {
            return _customerCity.Validate();
        }

        // Customer Postal Code
        private ValidatableObject<string> _customerPostalCode = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerPostalCode };

        public ValidatableObject<string> CustomerPostalCode
        {
            get { return _customerPostalCode; }
            set
            {
                _customerPostalCode = value;
                RaisePropertyChanged(() => CustomerPostalCode);
            }
        }

        private DelegateCommand _validateCustomerPostalCodeCommand;

        public DelegateCommand ValidateCustomerPostalCodeCommand =>
            _validateCustomerPostalCodeCommand ?? (_validateCustomerPostalCodeCommand = new DelegateCommand(() => ExecuteValidateCustomerPostalCodeCommand()));

        private bool ExecuteValidateCustomerPostalCodeCommand()
        {
            return _customerPostalCode.Validate();
        }

        // Customer Nearest Intersection
        private ValidatableObject<string> _customerNearestIntersection = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerNearestIntersection };

        public ValidatableObject<string> CustomerNearestIntersection
        {
            get { return _customerNearestIntersection; }
            set
            {
                _customerNearestIntersection = value;
                RaisePropertyChanged(() => CustomerNearestIntersection);
            }
        }

        private DelegateCommand _validateCustomerNearestIntersectionCommand;

        public DelegateCommand ValidateCustomerNearestIntersectionCommand =>
            _validateCustomerNearestIntersectionCommand ?? (_validateCustomerNearestIntersectionCommand = new DelegateCommand(() => ExecuteValidateCustomerNearestIntersectionCommand()));

        private bool ExecuteValidateCustomerNearestIntersectionCommand()
        {
            return _customerNearestIntersection.Validate();
        }

        //Customer Phone Number

        private ValidatableObject<string> _customerPhoneNumber = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerPhoneNumber };

        public ValidatableObject<string> CustomerPhoneNumber
        {
            get { return _customerPhoneNumber; }
            set
            {
                _customerPhoneNumber = value;
                RaisePropertyChanged(() => CustomerPhoneNumber);
            }
        }

        private DelegateCommand _validateCustomerPhoneNumberCommand;

        public DelegateCommand ValidateCustomerPhoneNumberCommand =>
            _validateCustomerPhoneNumberCommand ?? (_validateCustomerPhoneNumberCommand = new DelegateCommand(() => ExecuteValidateCustomerPhoneNumberCommand()));

        private bool ExecuteValidateCustomerPhoneNumberCommand()
        {
            return _customerPhoneNumber.Validate();
        }

        // Customer Cell Number

        private ValidatableObject<string> _customerCellNumber = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerCellNumber };

        public ValidatableObject<string> CustomerCellNumber
        {
            get { return _customerCellNumber; }
            set
            {
                _customerCellNumber = value;
                RaisePropertyChanged(() => CustomerCellNumber);
            }
        }

        private DelegateCommand _validateCustomerCellNumberCommand;

        public DelegateCommand ValidateCustomerCellNumberCommand =>
            _validateCustomerCellNumberCommand ?? (_validateCustomerCellNumberCommand = new DelegateCommand(() => ExecuteValidateCustomerCellNumberCommand()));

        private bool ExecuteValidateCustomerCellNumberCommand()
        {
            return _customerCellNumber.Validate();
        }

        // Customer Email Address
        private ValidatableObject<string> _customerEmail = new ValidatableObject<string>() { Value = string.Empty, Title = StringResources.CustomerEmail };

        public ValidatableObject<string> CustomerEmail
        {
            get { return _customerEmail; }
            set
            {
                _customerEmail = value;
                RaisePropertyChanged(() => CustomerEmail);
            }
        }

        private DelegateCommand _validateCustomerEmailCommand;

        public DelegateCommand ValidateCustomerEmailCommand =>
            _validateCustomerEmailCommand ?? (_validateCustomerEmailCommand = new DelegateCommand(() => ExecuteValidateCustomerEmailCommand()));

        private bool ExecuteValidateCustomerEmailCommand()
        {
            return _customerEmail.Validate();
        }

        #endregion

        #region Constructor
        public CustomerDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            AddValidations();
        }
        #endregion

        #region Methods
        private void AddValidations()
        {

            //Appliance Tab Validation
            _customerFirstName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerFirstNameRequired
            });
            _customerLastName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerLastNameRequired
            });

            _customerAddress.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerAddressRequired
            });
            _customerCity.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerCityRequired
            });
            _customerPhoneNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerPhoneNumberRequired
            });

            _customerCellNumber.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerCellNumberRequired
            });

            _customerEmail.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.CustomerCellNumberRequired
            });
             _customerEmail.Validations.Add(new EmailRule<string>
            {
                ValidationMessage = StringResources.CustomerEnterValidEmailAddress
             });

        }
        #endregion
    }
}
