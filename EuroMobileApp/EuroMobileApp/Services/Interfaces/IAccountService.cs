using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Models.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Authenticate(string username, string password);

    }
}
