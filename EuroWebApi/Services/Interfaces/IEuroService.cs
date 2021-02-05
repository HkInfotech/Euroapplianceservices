using EuroWebApi.Models;
using EuroWebApi.Models.Common;
using EuroWebApi.Models.Common.Request;
using EuroWebApi.Models.Common.Response;
using EuroWebApi.Models.ModelDTO;
using EuroWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EuroWebApi.Services.Interfaces
{
    public interface IEuroService
    {
        Response<LoginResponse> Authenticate(LoginRequest loginRequest);
        Response<List<OrdersViewModel>> GetOrdersByUserId(WorkOrderFilterRequest request);
        Response<List<ApplianceViewModel>> GetCustomerAppliancesByWorkOrderId(MobileRequest request);
        Response<List<ApplianceTypeViewModel>> GetApplianceTypes(MobileRequest request);
        Response<List<ManufacturerViewModel>> GetManufacturers(MobileRequest request);
        Response<List<JobNatureViewModel>> GetJobNatures(MobileRequest request);
        Response<List<JobStatusViewModel>> GetJobStatus(MobileRequest request);
        Response<List<TechnicianViewModel>> GetTechnicians(MobileRequest request);

        Response<WorkOrderDetailViewModel> GetWorkOrderDetailById(MobileRequest mobileRequest);
        Response<List<WorkOrderServiceViewModel>> GetWorkOrderServicesbyId(MobileRequest request);

        Response<List<WorkOrderPartViewModel>> GetWorkOrderPartsById(MobileRequest request);
        Response<WorkOrderTechMarkViewModel> GetWorkOrderTechRemarks(MobileRequest request);

        Response<bool> SaveWorkOrder(WorkOrderRequestModel request);
        Response<SaveWorkOrderPartResponse> SaveWorkOrderPart(WorkOrderPartViewModel workOrderRequestModel);
        Response<SaveWorkOrderPartResponse> DeleteWorkOrderPart(WorkOrderPartViewModel workOrderRequestModel);

        Response<bool> SaveSignature(SaveSignatureRequest request);
        Response<string> GetInvoiceText(InvoiceTextRequest request);
        Response<InvoiceTotalViewModel> GetInvoiceTotal(MobileRequest request);
        Response<CustomerInfoViewModel> UpdateCustomerInfo(CustomerInfoRequest customerRequest);
        Response<CustomerInfoViewModel> GetCustomerInfo(MobileRequest mobileRequest);
        Response<CustomerInvoiceSignatureViewModel> GetInvoiceSignatureInfo(MobileRequest mobileRequest);

        Response<List<WorkOrderImageViewModel>> GetWorkOrderImages(MobileRequest request);

        Response<AppConfigViewModel> GetCovidAppConfig(MobileRequest request);

    }
}