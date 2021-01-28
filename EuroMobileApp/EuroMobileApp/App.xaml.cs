using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Acr.UserDialogs;
using EuroMobileApp.Configurations;
using EuroMobileApp.Extensions;
using EuroMobileApp.Helpers;
using EuroMobileApp.Resources;
using EuroMobileApp.Services.Implements;
using EuroMobileApp.Services.Interfaces;
using EuroMobileApp.ViewModels;
using EuroMobileApp.Views;
using Plugin.Connectivity;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace EuroMobileApp
{
    public partial class App : PrismApplication
    {
        public App()
           : this(null)
        {
        }

        public App(IPlatformInitializer initializer)
            : base(initializer, true)
        {

        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            SetupLoggingListeners();
            
            IAppSettings appSettings = DependencyService.Get<IAppSettings>();
            //var container = (App.Current as PrismApplication).Container;
            //var navigationService = container.Resolve<INavigationService>();
            //var result = await NavigationService.NavigateAsync($"{PageName.Navigation}/{ PageName.OrderListPage}");
            //if (!result.Success)
            //{
            //    // I can now see the result.Exception is a ContainerResolutionException
            //    // and that it was the ViewModel not the View that had the issue...
            //    // thus telling me where to focus my energy... 
            //    //await navigationService.NavigateAsync($"{PageName.Navigation}/{ PageName.OrderListPage}");
            //}

            if (appSettings.IsLogin)
            {
                await NavigationService.NavigateAsync($"{PageName.Navigation}/{ PageName.OrderListPage}");

            }
            else
            {
                await NavigationService.NavigateAsync($"{PageName.Navigation}/{PageName.LoginPage}");

            }

           // await NavigationService.NavigateAsync($"{PageName.Navigation}/{PageName.SideMenuPage}");

        }
        public static Action StringResourceAction { get; set; }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();
            containerRegistry.RegisterPopupDialogService();

            containerRegistry.Register<Prism.Navigation.INavigationService, Prism.Navigation.PageNavigationService>();

            containerRegistry.RegisterForNavigation<NavigationPage>(PageName.Navigation);
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>(PageName.MainPage);
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>(PageName.LoginPage);
            containerRegistry.RegisterForNavigation<OrderListPage, OrderListPageViewModel>(PageName.OrderListPage);
            containerRegistry.RegisterForNavigation<WorkOrderDetailPage, WorkOrderDetailPageViewModel>(PageName.WorkOrderDetailPage);
            containerRegistry.RegisterForNavigation<WorkOrderPartDetailPage, WorkOrderPartDetailPageViewModel>(PageName.WorkOrderPartDetailPage);
            containerRegistry.RegisterForNavigation<ViewImagePage, ViewImagePageViewModel>(PageName.ViewImagePage);
            containerRegistry.RegisterForNavigation<WorkOrderSearchFilterPage, WorkOrderSearchFilterPageViewModel>(PageName.WorkOrderSearchFilterPage);
            containerRegistry.RegisterForNavigation<SideMenuPage, SideMenuPageViewModel>(PageName.SideMenuPage);

            // Instances
            containerRegistry.RegisterInstance(typeof(IUserDialogs), UserDialogs.Instance);
            containerRegistry.RegisterInstance(typeof(Plugin.Connectivity.Abstractions.IConnectivity), CrossConnectivity.Current);
            containerRegistry.RegisterInstance(typeof(ISettings), CrossSettings.Current);
            containerRegistry.RegisterInstance<IAppSettings>(new AppSettings());
            containerRegistry.RegisterInstance<IAppConfiguration>(new ProductionConfiguration());

            StringResourceAction = () =>
            {
                var platStrings = Activator.CreateInstance(typeof(strings), true);
                var platProperties = typeof(strings).GetProperties(BindingFlags.Static | BindingFlags.NonPublic);
                var finalProperties = typeof(StringResources).GetProperties(BindingFlags.Static | BindingFlags.Public);
                var resources = StringResources.Instance;
                foreach (var prop in finalProperties)
                {
                    var platProp = platProperties.FirstOrDefault(t => t.Name == prop.Name);
                    if (platProp != null)
                    {
                        prop.SetValue(resources, platProp.GetValue(platStrings));
                    }
                }
            };
            if (StringResourceAction != null)
            {
                StringResourceAction();
            }


            // Services
            containerRegistry.Register<IAccountService, AccountService>();
            containerRegistry.Register<IEuroMobileService, EuroMobileService>();
            containerRegistry.Register<IAggregatedServices, AggregatedServices>();


        }
        private void SetupLoggingListeners()
        {
            Log.Listeners.Add(new FormsLoggingListener());
            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Trace.WriteLine("Unobserved Task Exception:");
                Trace.WriteLine($"{e.Exception}");
            };
        }

        private class FormsLoggingListener : LogListener
        {
            public override void Warning(string category, string message) =>
                Trace.WriteLine($"    {category}: {message}");
        }
        public static void RequestPermissions()
        {


            var a = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            var b = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.Camera>();
            var c = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.StorageRead>();
            var d = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.StorageWrite>();
            var e = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.Photos>();
            var f = PermissionHelpers.RequestIfNeeded<Xamarin.Essentials.Permissions.Media>();

        }
    }
}
