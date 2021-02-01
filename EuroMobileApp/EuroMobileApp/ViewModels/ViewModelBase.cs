using Acr.UserDialogs;
using EuroMobileApp.Configurations;
using EuroMobileApp.Helpers;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.ViewModels.Base;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.AppModel;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroMobileApp.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ViewModelBase : ExtendedBindableObject, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {

        #region Services
        protected INavigationService NavigationService { get;  set; }
        protected IPageDialogService PageDialogService { get; set; }
        protected IUserDialogs UserDialogsService { get;  set; }
		protected IAccountService accountService { get;  set; }
		protected IAppConfiguration _appConfiguration { get;  set; }
		protected IEuroMobileService euroMobileService { get;  set; }

        protected IEventAggregator EventAggregator { get;  set; }



        private readonly Plugin.Connectivity.Abstractions.IConnectivity Connectivity = CrossConnectivity.Current;

        protected readonly IAppSettings _settings;
        #endregion
        #region Properties
        private string loadingText = "Loading...";
        public string LoadingText
        {
            get { return loadingText; }
            set { SetProperty(ref loadingText, value); }
        }

        private bool isEmpty;
        public bool IsEmpty
        {
            get { return isEmpty; }
            set { SetProperty(ref isEmpty, value); }
        }
        private string _emptyStateTitle = StringResources.EmptyStateDefaultTitle;
        public string EmptyStateTitle
        {
            get { return _emptyStateTitle; }
            set { SetProperty(ref _emptyStateTitle, value); }
        }

        private string _emptyStateSubtitle = StringResources.DefaultSubtitle;
        public string EmptyStateSubtitle
        {
            get { return _emptyStateSubtitle; }
            set { SetProperty(ref _emptyStateSubtitle, value); }
        }
        protected bool IsConnected { get; private set; }

        public static CancellationTokenSource CancellationToken { get; set; }
        #endregion

        #region Variable

        #endregion

        #region Commands
        
        #endregion

        #region Constrictors
        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            UserDialogsService = UserDialogs.Instance;
            IsConnected = Connectivity.IsConnected;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public ViewModelBase(IAggregatedServices aggregatedServices)
        {
            accountService = aggregatedServices.accountService;
            euroMobileService = aggregatedServices.euroMobileService;
            IsConnected = Connectivity.IsConnected;
            UserDialogsService = UserDialogs.Instance;

            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        public ViewModelBase(INavigationService navigationService, IPageDialogService pageDialogService, IAppSettings settings, IEventAggregator eventAggregator)
        {
            _settings = settings;
            EventAggregator = eventAggregator;
            NavigationService = navigationService;
            PageDialogService = pageDialogService;

            UserDialogsService = UserDialogs.Instance;
            IsConnected = Connectivity.IsConnected;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            CancellationToken = new CancellationTokenSource();
        }

        public ViewModelBase() : base()
        {
            UserDialogsService = UserDialogs.Instance;
            IsConnected = Connectivity.IsConnected;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
            CancellationToken = new CancellationTokenSource();

        }

        #endregion

        #region Methods
        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }
        public virtual void OnAppearing() { }

        public virtual void OnDisappearing() { }
        public virtual void Destroy()
        {

        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

           
            if (!IsConnected)
            {
                UserDialogsService.Toast(StringResources.Disconnectedfromthenetwork, TimeSpan.FromSeconds(2));
            }

        }
        protected Task DisplayAlertAsync(string message, string title = "")
        {
            return UserDialogsService.AlertAsync(message, title, StringResources.OK);
        }


        protected Task<bool> DisplayConfirmationAlertAsync(string message, string title = "", string accept = "", string cancel = "")
        {
            if (string.IsNullOrEmpty(title))
            {
                title = StringResources.Confirm;
            }

            if (string.IsNullOrEmpty(accept))
            {
                accept = StringResources.Yes;
            }

            if (string.IsNullOrEmpty(cancel))
            {
                cancel = StringResources.No;
            }

            return UserDialogsService.ConfirmAsync(title, message, accept, cancel);
        }

        protected Task<string> DisplayActionSheetAlertAsync(string[] buttons, string title = "")
        {
          //  DismissLoading();

            if (Device.RuntimePlatform == Device.iOS)
            {
                return UserDialogsService.ActionSheetAsync(title, StringResources.Cancel, null, null, buttons);
            }
            else
            {
                return UserDialogsService.ActionSheetAsync(title, StringResources.Cancel, "", null, buttons);
            }
        }

        #endregion

        //#region Services

        //#endregion

        //#region Properties

        //#endregion

        //#region Variable

        //#endregion

        //#region Command


        //#endregion

        //#region Constructor

        //#endregion

        //#region Methods

        //#endregion

    }
}
