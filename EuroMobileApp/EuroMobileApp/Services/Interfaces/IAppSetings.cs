using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Services.Interfaces
{
    public interface IAppSettings
    {
        bool IsOnline { get; set; }
        bool IsLogin { get; set; }
        string BaseUrl { get; set; }
        string Token { get; set; }
        IUser CurrentUser { get; set; }

         string FullName { get; set; }
         string Password { get; set; }
         string Username { get; set; }
         string Email { get; set; }
         string UserId { get; set; }
        string UserTypeId { get; set; }
         bool Inactive { get; set; }
        void ResetDetails();

        //IUser CurrentUser { get; set; }
    }
}
