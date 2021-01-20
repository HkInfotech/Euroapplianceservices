using EuroMobileApp.Configurations;
using EuroMobileApp.Helpers;
using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Models.Common.Response;
using EuroMobileApp.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EuroMobileApp.Services.Implements
{
    public class EuroMobileService : IEuroMobileService
    {
        private readonly RestApiResponseHelper _restClient;
        private readonly IAppConfiguration appConfiguration;
        private readonly IAppSettings _setting;

        public EuroMobileService(IAppConfiguration appConfiguration, IAppSettings appSettings)
        {
            _restClient = new RestApiResponseHelper();
            _setting = appSettings;
            this.appConfiguration = appConfiguration;
        }

        public async Task<List<ApplianceTypeModel>> GetApplianceTypes()
        {
            MobileRequest mobileRequest = new MobileRequest();
            //mobileRequest.UserId = Convert.ToString(_setting.CurrentUser.UserId);
            //mobileRequest.Username = _setting.CurrentUser.Username;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<ApplianceTypeModel>>(Endpoint.GetApplianceTypes, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<ApplianceModel>> GetCustomerAppliancesByWorkOrderId(int OrderId)
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            mobileRequest.Id = OrderId;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<ApplianceModel>>(Endpoint.GetCustomerAppliancesByWorkOrderId, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<JobNatureModel>> GetJobNatures()
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<JobNatureModel>>(Endpoint.GetJobNatures, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<JobStatusModel>> GetJobStatus()
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<JobStatusModel>>(Endpoint.GetJobStatus, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<ManufacturerModel>> GetManufacturers()
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<ManufacturerModel>>(Endpoint.GetManufacturers, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<OrderModel>> GetOrdersByUserId()
        {
            try
            {
                MobileRequest mobileRequest = new MobileRequest();
                mobileRequest.UserId = _setting.UserId;
                mobileRequest.Username = _setting.Username;
                var json = JsonConvert.SerializeObject(mobileRequest);
                var response = await _restClient.PostAsync<List<OrderModel>>(Endpoint.GetOrdersByUserId, json, appConfiguration.BaseUrl).ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                return new List<OrderModel>();
            }

        }
        public async Task<List<OrderModel>> GetOrdersByFilter(WorkOrderFilterRequest request)
        {
            try
            {
                request.FirstName = string.IsNullOrEmpty(request.FirstName) ? "" : request.FirstName;
                request.LastName = string.IsNullOrEmpty(request.LastName) ? "" : request.LastName;
                request.PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? "" : request.PhoneNumber;
                request.WorkOrderId = string.IsNullOrEmpty(request.OrderId) ? 0 : Convert.ToInt64(request.OrderId);
                var json = JsonConvert.SerializeObject(request);
                var response = await _restClient.PostAsync<List<OrderModel>>(Endpoint.GetOrdersByUserId, json, appConfiguration.BaseUrl).ConfigureAwait(false);
                return response;
            }
            catch (Exception ex)
            {
                return new List<OrderModel>();
            }

        }
        public async Task<List<TechnicianModel>> GetTechnicians()
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<TechnicianModel>>(Endpoint.GetTechnicians, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<WorkOrderDetailModel> GetWorkOrderDetailById(int OrderId)
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            mobileRequest.Id = OrderId;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<WorkOrderDetailModel>(Endpoint.GetWorkOrderDetailById, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<WorkOrderPartModel>> GetWorkOrderPartsById(int OrderId)
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            mobileRequest.Id = OrderId;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<WorkOrderPartModel>>(Endpoint.GetWorkOrderPartsById, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }

        public async Task<List<WorkOrderServiceModel>> GetWorkOrderServicesbyId(int OrderId)
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            mobileRequest.Id = OrderId;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<List<WorkOrderServiceModel>>(Endpoint.GetWorkOrderServicesbyId, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }
        public async Task<WorkOrderTechMarkModel> GetWorkOrderTechRemarks(int OrderId)
        {
            MobileRequest mobileRequest = new MobileRequest();
            mobileRequest.UserId = _setting.UserId;
            mobileRequest.Username = _setting.Username;
            mobileRequest.Id = OrderId;
            var json = JsonConvert.SerializeObject(mobileRequest);
            var response = await _restClient.PostAsync<WorkOrderTechMarkModel>(Endpoint.GetWorkOrderTechRemarks, json, appConfiguration.BaseUrl).ConfigureAwait(false);
            return response;
        }
        public async Task<bool> SaveWorkOrder(WorkOrderRequestModel workOrderRequest)
        {
            try
            {
                var json = JsonConvert.SerializeObject(workOrderRequest);
                var response = await _restClient.PostAsync<bool>(Endpoint.SaveWorkOrder, json, appConfiguration.BaseUrl).ConfigureAwait(false);

                return response;


            }
            catch (Exception)
            {
                return true;

                throw;
            }
        }
        public async Task<SaveWorkOrderPartResponse> SaveWorkOrderPart(WorkOrderPartModel workOrderPart)
        {
            try
            {
                var json = JsonConvert.SerializeObject(workOrderPart);
                var response = await _restClient.PostAsync<SaveWorkOrderPartResponse>(Endpoint.SaveWorkOrderPart, json, appConfiguration.BaseUrl).ConfigureAwait(false);
                return response;

            }
            catch (Exception)
            {
                return new SaveWorkOrderPartResponse();
            }
        }
        public async Task<SaveWorkOrderPartResponse> DeleteWorkOrderPart(WorkOrderPartModel workOrderPart)
        {
            try
            {
                var json = JsonConvert.SerializeObject(workOrderPart);
                var response = await _restClient.PostAsync<SaveWorkOrderPartResponse>(Endpoint.DeleteWorkOrderPart, json, appConfiguration.BaseUrl).ConfigureAwait(false);
                return response;

            }
            catch (Exception)
            {
                return new SaveWorkOrderPartResponse();
            }
        }
    }
}
