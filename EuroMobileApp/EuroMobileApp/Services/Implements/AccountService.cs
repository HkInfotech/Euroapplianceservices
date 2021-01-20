using EuroMobileApp.Configurations;
using EuroMobileApp.Helpers;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Models.Common.Response;
using EuroMobileApp.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.Services.Implements
{
    public class AccountService : IAccountService
    {
        private readonly RestApiResponseHelper _restClient;
        private readonly IAppConfiguration appConfiguration;
        public AccountService(IAppConfiguration appConfiguration)
        {
            _restClient = new RestApiResponseHelper();
            this.appConfiguration = appConfiguration;
        }
        public async Task<LoginResponse> Authenticate(string username, string password)
        {

            LoginResponse response = new LoginResponse();
            try
            {
                LoginRequest loginRequest = new LoginRequest();
                loginRequest.Username = username;
                loginRequest.Password = password;
                var json = JsonConvert.SerializeObject(loginRequest);
                response = await _restClient.PostAsync<LoginResponse>(Endpoint.Authenticate,json, appConfiguration.BaseUrl).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error Authenticate User  {ex}");
            }
            return response;
        }
    }
}
