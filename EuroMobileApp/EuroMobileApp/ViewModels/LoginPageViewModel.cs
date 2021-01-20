using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.Validations;
using EuroMobileApp.Views;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroMobileApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {


        #region Services

        #endregion
        #region Properties and It's Command

        private ValidatableObject<string> _userName = new ValidatableObject<string>() { Value = string.Empty };


        public ValidatableObject<string> UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }


        private DelegateCommand _validateUserNameCommand;
        public DelegateCommand ValidateUserNameCommand =>
            _validateUserNameCommand ?? (_validateUserNameCommand = new DelegateCommand(() => ExecuteValidateUserNameCommand()));

        bool ExecuteValidateUserNameCommand()
        {
            return _userName.Validate();
        }

        private ValidatableObject<string> _userPassword = new ValidatableObject<string>() { Value = string.Empty };
        public ValidatableObject<string> UserPassword
        {
            get { return _userPassword; }
            set
            {
                _userPassword = value;
                RaisePropertyChanged(() => UserPassword);
            }
        }
        private DelegateCommand _validateUserPasswordCommand;
        public DelegateCommand ValidateUserPasswordCommand =>
            _validateUserPasswordCommand ?? (_validateUserPasswordCommand = new DelegateCommand(() => ExecuteValidateUserPasswordCommand()));

        bool ExecuteValidateUserPasswordCommand()
        {
            return _userPassword.Validate();
        }




        #endregion

        #region Variable

        #endregion

        #region Commands
        private DelegateCommand _loginCommand;
        private readonly IAccountService _accountService;

        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(async () => await ExecuteLoginCommand()));

        async private Task ExecuteLoginCommand()
        {
            ValidateFields();
            if (IsConnected == true)
            {
                if (UserName.IsValid && UserPassword.IsValid)
                {
                    IsBusy = true;
                    var result = await _accountService.Authenticate(UserName.Value, UserPassword.Value);
                    if (result.UserId != 0)
                    {
                        IUser user = new User();
                        user.Email = "";
                        _settings.Password = UserPassword.Value;
                        _settings.Username = UserName.Value;
                        _settings.FullName = result.UserFullName;
                        _settings.UserId = Convert.ToString(result.UserId);
                        _settings.UserTypeId = Convert.ToString(result.UserTypeId);
                        _settings.Inactive = result.Inactive;
                        _settings.CurrentUser = user;
                        _settings.IsLogin = true;
                        IsBusy = false;

                        await NavigationServiceExtensions.TryNavigateModallyAsync(NavigationService, $"/{PageName.Navigation}/{PageName.OrderListPage}", null);
                    }
                    else
                    {
                        IsBusy = false;
                        await UserDialogsService.AlertAsync($"{StringResources.InvalidLoginError}", null, StringResources.OK);
                    }
                }                
            }
            else
            {
                IsBusy = false;

                await UserDialogsService.AlertAsync($"{StringResources.OfflineNotAvailableForResource}", null, StringResources.OK);

            }

        }
        #endregion

        #region Constrictors
        public LoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IAppSettings settings, IEventAggregator eventAggregator, IAccountService accountService) : base(navigationService, pageDialogService, settings, eventAggregator)
        {
#if DEBUG
            UserName = new ValidatableObject<string>() { Value = "ODEAN" };
            UserPassword = new ValidatableObject<string>() { Value = "123456" };
#else
            UserName = new ValidatableObject<string>() { Value = "" };
            UserPassword = new ValidatableObject<string>() { Value = "" };
#endif

            AddValidations();
            this._accountService = accountService;
        }
        #endregion

        #region Methods
        public void ValidateFields()
        {
            ValidateUserPasswordCommand.Execute();
            ValidateUserNameCommand.Execute();
        }
        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.UserNameRequired
            });
            _userPassword.Validations.Add(new IsNotNullOrEmptyRule<string>
            {
                ValidationMessage = StringResources.UserPasswordRequired
            });

        }

        #endregion
        //private bool _isBusy { get; set; }
        //public bool isBusy
        //{
        //    get { return _isBusy; }
        //    set
        //    {
        //        if ((!_isBusy) && value)
        //            Application.Current.MainPage.Navigation.PushPopupAsync(new PopupLoading());
        //        else if (_isBusy && (!value))
        //            Application.Current.MainPage.Navigation.PopPopupAsync();
        //        _isBusy = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public LoginModel loginDetails { get; set; }

        //public LoginPageViewModel(INavigationService navigationServise) : base(navigationServise)
        //{

        //}

        //public Command LoginCommand
        //{
        //    get
        //    {
        //        return new Command(() =>
        //        {
        //            if (!CrossConnectivity.Current.IsConnected)
        //            {
        //                Application.Current.MainPage.DisplayAlert("Alert", "No Internet connection. Make sure that Wifi or mobile data is turned on, then try again", "OK");
        //            }
        //            else
        //            {
        //                if (loginDetails.data.LoginName != null && loginDetails.data.UserPassword != null)
        //                {
        //                    Logindatafunction(loginDetails.data.LoginName, loginDetails.data.UserPassword);
        //                }

        //                else
        //                {
        //                    Application.Current.MainPage.DisplayAlert("Error", "Enter Email Or Password!", "Ok");
        //                    //txtemail.Text = "";
        //                    //txtpass.Text = "";
        //                    //txtemail.Focus();
        //                }
        //            }
        //        });
        //    }
        //}

        //public async void Logindatafunction(string username, string password)
        //{
        //    //App.Current.Properties["Role"] = loginDetails.data.UserTypeId;
        //    //App.Current.Properties["username"] = username;
        //    //App.Current.Properties["password"] = password;
        //    //Application.Current.MainPage = new NavigationPage(new OrdersListPage());

        //    try
        //    {
        //        isBusy = true;

        //        string rolename = string.Empty;
        //        var formcontent = new FormUrlEncodedContent(new[]
        //        {
        //         new KeyValuePair<string,string>("userName",username),
        //         new KeyValuePair<string,string>("password",password)
        //        });
        //        var client = new HttpClient();
        //        string url = GlobalApiMaintain.EuroApiUrl + "/EuroApi/Login?";
        //        var result = await client.PostAsync(url, formcontent);
        //        var json = await result.Content.ReadAsStringAsync();
        //        loginDetails = JsonConvert.DeserializeObject<LoginModel>(json.ToString());
        //        if (!loginDetails.status)
        //        {
        //            isBusy = false;
        //            await Application.Current.MainPage.DisplayAlert("Error", "You not authorized to login.", "Ok");
        //            return;
        //        }
        //        else
        //        {
        //            App.Current.Properties["Role"] = loginDetails.data.UserTypeId.ToString();
        //            App.Current.Properties["userid"] = loginDetails.data.UserId.ToString();
        //            App.Current.Properties["username"] = username;
        //            App.Current.Properties["password"] = password;
        //            await App.Current.SavePropertiesAsync();
        //            isBusy = false;
        //            Application.Current.MainPage = new NavigationPage(new OrdersListPage());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //await DisplayAlert("Error", "ex.Message.ToString()", "Ok");
        //        isBusy = false;
        //        await Application.Current.MainPage.DisplayAlert("Error", "Please Enter Valid  Email and password!", "Ok");
        //        //txtemail.Text = "";
        //        //txtpass.Text = "";
        //        //txtemail.Focus();
        //        return;
        //    }
        //}
    }
}
