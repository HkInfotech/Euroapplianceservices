using EuroMobileApp.Models;
using EuroMobileApp.Services.Implements;
using EuroMobileApp.Services.Interfaces;
using MvvmHelpers;
using Newtonsoft.Json;
using Plugin.SecureStorage;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(AppSettings))]
namespace EuroMobileApp.Services.Implements
{
    public class AppSettings : BaseViewModel,IAppSettings, INotifyPropertyChanged
    {
        /// <summary>
        /// Settings service
        /// </summary>
        //private static Lazy<Settings> SettingsInstance = new Lazy<Settings>(() => new Settings());

        /// <summary>
        /// Instance
        /// </summary>
        //public static Settings Current => SettingsInstance.Value;

        private ISettings AppSetting
        {
            get { return CrossSettings.Current; }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void ResetDetails()
        {
            IsLogin = false;
            Token = null;
            CurrentUser = new User();
            FullName = "";
            Password = "";
            UserId = "";
            Username = "";
            Inactive = true;
        }

        public bool IsOnline
        {
            get => AppSetting.GetValueOrDefault(nameof(IsOnline), default(bool));
            set => AppSetting.AddOrUpdateValue(nameof(IsOnline), value);
        }
        public bool IsLogin
        {
            get => AppSetting.GetValueOrDefault(nameof(IsLogin), default(bool));
            set => AppSetting.AddOrUpdateValue(nameof(IsLogin), value);
        }
        public string BaseUrl
        {
            get => AppSetting.GetValueOrDefault(nameof(BaseUrl), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(BaseUrl), value);
        }
        public IUser CurrentUser
        {
            get
            {
                var json = CrossSecureStorage.Current.GetValue(nameof(CurrentUser));
                if (json != null)
                {
                    return JsonConvert.DeserializeObject<User>(json);
                }

                return null;
            }
            set
            {
                CrossSecureStorage.Current.SetValue(nameof(CurrentUser), JsonConvert.SerializeObject(value));
            }
        }

        public string Token
        {
            get => AppSetting.GetValueOrDefault(nameof(Token), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(Token), value);
        }

        public string FullName
        {
            get => AppSetting.GetValueOrDefault(nameof(FullName), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(FullName), value);
        }

        public string Password
        {
            get => AppSetting.GetValueOrDefault(nameof(Password), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(Password), value);
        }
        public string Username
        {
            get => AppSetting.GetValueOrDefault(nameof(Username), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(Username), value);
        }
        public string Email
        {
            get => AppSetting.GetValueOrDefault(nameof(Email), string.Empty);
            set => AppSetting.AddOrUpdateValue(nameof(Email), value);
        }
        public string UserId
        {
            get => AppSetting.GetValueOrDefault(nameof(UserId), "0");
            set => AppSetting.AddOrUpdateValue(nameof(UserId), value);
        }

        public string UserTypeId
        {
            get => AppSetting.GetValueOrDefault(nameof(UserTypeId), "0");
            set => AppSetting.AddOrUpdateValue(nameof(UserTypeId), value);
        }
        public bool Inactive
        {
            get => AppSetting.GetValueOrDefault(nameof(Inactive), false);
            set => AppSetting.AddOrUpdateValue(nameof(Inactive), value);
        }
        //public IUser CurrentUser
        //{
        //    get
        //    {
        //        var json = CrossSecureStorage.Current.GetValue(nameof(CurrentUser));
        //        if (json != null)
        //        {
        //            return JsonConvert.DeserializeObject<User>(json);
        //        }

        //        return null;
        //    }
        //    set
        //    {
        //        CrossSecureStorage.Current.SetValue(nameof(CurrentUser), JsonConvert.SerializeObject(value));
        //    }
        //}

        #endregion INotifyPropertyChanged
    }
}
