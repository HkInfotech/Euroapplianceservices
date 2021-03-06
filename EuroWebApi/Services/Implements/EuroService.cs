﻿using EuroWebApi.Models;
using EuroWebApi.Models.Common;
using EuroWebApi.Models.Common.Request;
using EuroWebApi.Models.Common.Response;
using EuroWebApi.Models.ModelDTO;
using EuroWebApi.Services.Interfaces;
using EuroWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace EuroWebApi.Services.Implements
{
    public class EuroService : IEuroService
    {
        readonly EURODbContext db;
        public EuroService()
        {
            this.db = new EURODbContext();
        }
        public Response<LoginResponse> Authenticate(LoginRequest loginRequest)
        {
            var response = new Response<LoginResponse>() { Success = true };
            try
            {
                if (loginRequest.Username == null || loginRequest.Password == null)
                {
                    response.Fail("Username or password incorrect");
                    return response;
                }
                var result = db.sp_webapi_CheckLogin(loginRequest.Username, loginRequest.Password)?.FirstOrDefault();
                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.LoginName))
                    {
                        if (result.LoginName.Equals("fail", StringComparison.OrdinalIgnoreCase))
                        {
                            response.Success = false;
                            response.Message = result.status;

                        }
                        else
                        {
                            LoginResponse loginResponse = new LoginResponse();
                            loginResponse.status = result.status;
                            loginResponse.UserId = result.UserId;
                            loginResponse.UserFullName = result.UserFullName;
                            loginResponse.UserTypeId = result.UserTypeId ?? 0;
                            response.ResponseContent = loginResponse;
                        }
                    }
                    else
                    {
                        response.Fail(result.status);
                    }
                }
                else
                {
                    response.Fail("Username or password incorrect");
                }

            }
            catch (Exception ex)
            {
                bool loginValid = false;
                response.Fail("Username or password incorrect");
            }
            return response;

        }

        public Response<List<ApplianceTypeViewModel>> GetApplianceTypes(MobileRequest request)
        {
            var response = new Response<List<ApplianceTypeViewModel>>() { Success = true };
            try
            {

                List<ApplianceTypeViewModel> applianceTypes = new List<ApplianceTypeViewModel>();
                var result = db.sp_webapi_GetApplianceTypes()?.ToList() ?? new List<sp_webapi_GetApplianceTypes_Result>();
                if (result.Count > 0)
                {
                    applianceTypes = result?.Select(a => new ApplianceTypeViewModel()
                    {
                        ApplianceName = a.ApplianceType,
                        ApplianceTypeId = a.ApplianceTypeId
                    })?.ToList() ?? new List<ApplianceTypeViewModel>();
                }
                response.ResponseContent = applianceTypes;
            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<ApplianceViewModel>> GetCustomerAppliancesByWorkOrderId(MobileRequest request)
        {
            var response = new Response<List<ApplianceViewModel>>() { Success = true };
            try
            {
                List<ApplianceViewModel> appliances = new List<ApplianceViewModel>();
                var GetAppliances = db.SP_webapi_GetCustomerAppliances(request.Id)?.ToList() ?? new List<SP_webapi_GetCustomerAppliances_Result>();
                if (GetAppliances.Any())
                {
                    appliances = GetAppliances.Select(a => new ApplianceViewModel()
                    {
                        ApplianceName = a.Appliance,
                        ApplianceTypeId = a.ApplianceTypeId ?? 0,
                        CustomerApplianceId = a.CustomerApplianceId,
                        CustomerId = a.CustomerId ?? 0,
                        CustomerName = a.CustomerName,
                        ManufacturerName = a.Manufacturer,
                        ManufacturerId = a.ManufacturerId ?? 0,
                        ModelImage = a.ModelImage,
                        ModelNumber = a.ModelNumber,
                        SerialNumber = a.SerialNumber,
                        UserId = 0,
                        WorkorderId = a.WorkOrderID ?? 0,
                        ImageFile1 = a.ImageFile1,
                        ImageFile2 = a.ImageFile2,
                        ImageFile3 = a.ImageFile3,
                        ImageFile4 = a.ImageFile4
                    }).ToList();
                    response.ResponseContent = appliances;
                }

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }

            return response;
        }



        public Response<List<ManufacturerViewModel>> GetManufacturers(MobileRequest request)
        {
            var response = new Response<List<ManufacturerViewModel>>() { Success = true };
            try
            {

                List<ManufacturerViewModel> manufacturers = new List<ManufacturerViewModel>();
                var result = db.sp_webapi_GetManufactures()?.ToList();
                if (result.Count > 0)
                {
                    manufacturers = result?.Select(a => new ManufacturerViewModel()
                    {
                        ManufacturerId = a.ManufacturerId,
                        ManufacturerName = a.Manufacturer
                    })?.ToList() ?? new List<ManufacturerViewModel>();
                }
                response.ResponseContent = manufacturers;
                return response;
                //var result=db.sp_webapi_GetManufactures()

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<OrdersViewModel>> GetOrdersByUserId(WorkOrderFilterRequest request)
        {
            var response = new Response<List<OrdersViewModel>>() { Success = true };
            try
            {

                List<OrdersViewModel> orders = new List<OrdersViewModel>();
                var result = db.sp_webapi_GetOrdersByUserID(Convert.ToInt64(request.UserId), request.WorkOrderId, request.FirstName,request.LastName,request.PhoneNumber)?.ToList() ?? new List<sp_webapi_GetOrdersByUserID_Result>();
                if (result.Count > 0)
                {

                    orders = result.Select(a => new OrdersViewModel() { ApplianceType = a.ApplianceType, CustomerName = a.CustomerName, JobStatus = a.JobStatus, message = "", UserId = a.UserId ?? Convert.ToInt64(request.UserId), WorkOrderDate = Convert.ToDateTime(a.WorkOrderDate), WorkOrderId = a.WorkOrderId }).ToList();
                    response.ResponseContent = orders;
                }
                else
                {
                    response.ResponseContent = orders;
                }
            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }


        public Response<List<JobNatureViewModel>> GetJobNatures(MobileRequest request)
        {
            var response = new Response<List<JobNatureViewModel>>() { Success = true };
            try
            {

                List<JobNatureViewModel> items = new List<JobNatureViewModel>();
                var result = db.sp_webapi_getJobNatureList()?.ToList();
                if (result.Count > 0)
                {
                    items = result?.Select(a => new JobNatureViewModel()
                    {
                        JobNature = a.JobNature,
                        JobNatureId = a.JobNatureId
                    })?.ToList() ?? new List<JobNatureViewModel>();
                }
                response.ResponseContent = items;
                return response;
                //var result=db.sp_webapi_GetManufactures()

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<JobStatusViewModel>> GetJobStatus(MobileRequest request)
        {
            var response = new Response<List<JobStatusViewModel>>() { Success = true };
            try
            {

                List<JobStatusViewModel> items = new List<JobStatusViewModel>();
                var result = db.sp_webapi_getJobStatusList()?.ToList();
                if (result.Count > 0)
                {
                    items = result?.Select(a => new JobStatusViewModel()
                    {
                        JobStatus = a.JobStatus,
                        JobStatusId = a.JobStatusId
                    })?.ToList() ?? new List<JobStatusViewModel>();
                }
                response.ResponseContent = items;
                return response;
            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<TechnicianViewModel>> GetTechnicians(MobileRequest request)
        {
            var response = new Response<List<TechnicianViewModel>>() { Success = true };
            try
            {

                List<TechnicianViewModel> items = new List<TechnicianViewModel>();
                var result = db.sp_webapi_getTechniciansList()?.ToList();
                if (result.Count > 0)
                {
                    items = result?.Select(a => new TechnicianViewModel()
                    {
                        TechnicanName = a.TechnicanName,
                        UserId = a.UserId
                    })?.ToList() ?? new List<TechnicianViewModel>();
                }
                response.ResponseContent = items;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<WorkOrderDetailViewModel> GetWorkOrderDetailById(MobileRequest mobileRequest)
        {
            var response = new Response<WorkOrderDetailViewModel>() { Success = true };
            try
            {

                WorkOrderDetailViewModel item = new WorkOrderDetailViewModel();
                var result = db.SP_webapi_GetOrderDetailsByWorkOrderId(mobileRequest.Id, Convert.ToInt64(mobileRequest.UserId))?.FirstOrDefault();
                if (result != null)
                {
                    item = new WorkOrderDetailViewModel()
                    {
                        COD_WARN = result.COD_WARN,
                        WorkOrderId = result.WorkOrderId,
                        CustomerName = result.CustomerName,
                        JobNature = result.JobNature,
                        JobNatureId = result.JobNatureId,
                        JobStatus = result.JobStatus,
                        JobStatusId = result.JobStatusId,
                        Mileage = result.Mileage,
                        Notes = result.Notes,
                        ServiceDate = result.ServiceDate,
                        ServiceTime = result.ServiceTime,
                        TicketNumber = result.TicketNumber,
                        UserFullName = result.UserFullName,
                        UserId = result.UserId
                    };
                    response.ResponseContent = item;
                }
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<WorkOrderServiceViewModel>> GetWorkOrderServicesbyId(MobileRequest request)
        {
            var response = new Response<List<WorkOrderServiceViewModel>>() { Success = true };
            try
            {
                List<WorkOrderServiceViewModel> items = new List<WorkOrderServiceViewModel>();
                var result = db.sp_webapi_GetWorkOrderServices(request.Id)?.ToList();
                if (result.Count > 0)
                {
                    items = result?.Select(a => new WorkOrderServiceViewModel()
                    {
                        ServiceItem = a.ServiceItem,
                        ServiceItemDescr = a.ServiceItemDescr,
                        ServiceItemid = a.ServiceItemid,
                        ServiceItemPrice = a.ServiceItemPrice
                    })?.ToList() ?? new List<WorkOrderServiceViewModel>();
                }
                response.ResponseContent = items;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<List<WorkOrderPartViewModel>> GetWorkOrderPartsById(MobileRequest request)
        {
            var response = new Response<List<WorkOrderPartViewModel>>() { Success = true };
            try
            {
                List<WorkOrderPartViewModel> items = new List<WorkOrderPartViewModel>();
                var result = db.SP_webapiGetWorkorderparts(request.Id)?.ToList() ?? new List<SP_webapiGetWorkorderparts_Result>(); ;
                if (result.Count > 0)
                {
                    items = result?.Select(a => new WorkOrderPartViewModel()
                    {
                        WorkOrderPartID = a.WorkOrderPartID,
                        InvoicePrice = a.InvoicePrice,
                        Notes = a.Notes,
                        PartName = a.PartName,
                        PurchasePrice = a.PurchasePrice,
                        Quantity = a.Quantity,
                        Rate = a.Rate,
                        WorkOrderId = a.WorkOrderId
                    })?.ToList() ?? new List<WorkOrderPartViewModel>();
                }
                response.ResponseContent = items;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<WorkOrderTechMarkViewModel> GetWorkOrderTechRemarks(MobileRequest request)
        {
            var response = new Response<WorkOrderTechMarkViewModel>() { Success = true };
            try
            {

                WorkOrderTechMarkViewModel item = new WorkOrderTechMarkViewModel();
                var result = db.SP_webapi_getTechRemarks(request.Id)?.FirstOrDefault();
                item = new WorkOrderTechMarkViewModel()
                {
                    WorkOrderTechRemarkNote = result
                };
                response.ResponseContent = item;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<bool> SaveWorkOrder(WorkOrderRequestModel request)
        {
            var response = new Response<bool>() { Success = true };
            try
            {
                using (var db = new EURODbContext())
                {
                    db.sp_webapi_UpdateTechRemarks(request.WorkOrderId, request.WorkOrderTechRemark);

                    var UpdateWorkOrderDetailResult = db.sp_webapi_UpdateOrderDetailsByWorkOrderId(request.WorkOrderId, request.UserId, request.ServiceDate, request.ServiceTime, request.JobNatureId, request.JobStatusId, request.TicketNumber, request.COD_WARN, request.Mileage, request.WorkOrderServiceNote, request.CustomerName);
                    List<string> DocumentPath = new List<string>();
                    string[] images = { "", "", "", "" };

                    if (request.Documents?.Count > 0)
                    {
                        for (int i = 0; i < request.Documents.Count; i++)
                        {
                            if (request.Documents[i].FileBlob != null)
                            {
                                string fileName = request.Documents[i].Name;
                                string filePath = "~/Uploads/Images/" + fileName;
                                var ServerPath = System.Web.Hosting.HostingEnvironment.MapPath(filePath);
                                System.IO.File.WriteAllBytes(ServerPath, request.Documents[i].FileBlob);
                                images[i] = "/Uploads/Images/" + fileName;
                            }
                            else
                            {

                                images[i] = request.Documents[i].ServerDocumentPath;
                            }
                        }
                    }

                    var servicesXML = Serialize(request.WorkOrderServices);
                    var SaveWorkOrderService = db.sp_webapi_UpdateWorkOrderServiceItems(request.WorkOrderId, servicesXML);
                    var UpdateWorkOrderAppliances = db.sp_webapi_update_appliance(request.WorkOrderId, Convert.ToInt32(request.CustomerApplianceId), Convert.ToInt32(request.ApplianceTypeId), Convert.ToInt32(request.ManufacturerId), request.SerialNumber, request.ModelNumber, images[0], images[1], images[2], images[3]);
                };


                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }
        private string ReturnLastStringFormImageUrl(string ImageUrl)
        {
            return ImageUrl.Split('/').LastOrDefault();
        }
        public Response<SaveWorkOrderPartResponse> SaveWorkOrderPart(WorkOrderPartViewModel workOrderRequestModel)
        {
            var response = new Response<SaveWorkOrderPartResponse>() { Success = true };
            try
            {
                var result = db.sp_webapi_add_update_workorderparts(workOrderRequestModel.WorkOrderId, workOrderRequestModel.UserId, workOrderRequestModel.PartName, workOrderRequestModel.Quantity, workOrderRequestModel.Rate, workOrderRequestModel.PurchasePrice, workOrderRequestModel.Notes, workOrderRequestModel.WorkOrderPartID)?.FirstOrDefault() ?? new sp_webapi_add_update_workorderparts_Result();
                SaveWorkOrderPartResponse saveWorkOrderPartResponse = new SaveWorkOrderPartResponse()
                {
                    Status = result.Status,
                    UserId = result.UserId,
                    WorKorderId = result.WorKorderId,
                    WorkOrderPartId = result.WorkOrderPartId
                };
                response.ResponseContent = saveWorkOrderPartResponse;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        public Response<SaveWorkOrderPartResponse> DeleteWorkOrderPart(WorkOrderPartViewModel workOrderRequestModel)
        {
            var response = new Response<SaveWorkOrderPartResponse>() { Success = true };
            try
            {
                var result = db.sp_webapi_delete_workorderparts(Convert.ToInt32(workOrderRequestModel.WorkOrderPartID), Convert.ToInt32(workOrderRequestModel.WorkOrderId), workOrderRequestModel.UserId)?.FirstOrDefault() ?? new sp_webapi_delete_workorderparts_Result();
                SaveWorkOrderPartResponse saveWorkOrderPartResponse = new SaveWorkOrderPartResponse()
                {
                    Status = result.status,
                    UserId = result.UserId,
                    WorKorderId = result.WorkOrderId,
                };
                response.ResponseContent = saveWorkOrderPartResponse;
                return response;

            }
            catch (Exception ex)
            {
                response.Fail(ex.Message);
            }
            return response;
        }

        private string Serialize<T>(T dataToSerialize)
        {
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stringwriter, dataToSerialize);
                return stringwriter.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            }
            catch
            {
                throw;
            }
        }
    }
}