using EuroMobileApp.Models;
using EuroMobileApp.Models.Common.Request;
using EuroMobileApp.Models.Common.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EuroMobileApp.Services.Interfaces
{
    public interface IEuroMobileService
    {
        Task<List<OrderModel>> GetOrdersByUserId();
        Task<List<OrderModel>> GetOrdersByFilter(WorkOrderFilterRequest request);
        Task<List<ApplianceTypeModel>> GetApplianceTypes();
        Task<List<ManufacturerModel>> GetManufacturers();
        Task<List<ApplianceModel>> GetCustomerAppliancesByWorkOrderId(int OrderId);
        Task<List<JobNatureModel>> GetJobNatures();
        Task<List<JobStatusModel>> GetJobStatus();
        Task<List<TechnicianModel>> GetTechnicians();
        Task<WorkOrderDetailModel> GetWorkOrderDetailById(int OrderId);
        Task<List<WorkOrderServiceModel>> GetWorkOrderServicesbyId(int OrderId);
        Task<List<WorkOrderPartModel>> GetWorkOrderPartsById(int OrderId);
        Task<WorkOrderTechMarkModel> GetWorkOrderTechRemarks(int OrderId);
        Task<bool> SaveWorkOrder(WorkOrderRequestModel workOrderRequest);
        Task<SaveWorkOrderPartResponse> SaveWorkOrderPart(WorkOrderPartModel workOrderPart);
        Task<SaveWorkOrderPartResponse> DeleteWorkOrderPart(WorkOrderPartModel workOrderPart);
        Task<CustomerModel> GetCustomerInfo(int customerId);
        Task<bool> SaveSignature(SaveSignatureRequest request);
        Task<string> GetInvoiceText(string textType);
        Task<CustomerModel> UpdateCustomerInfo(CustomerInfoRequest request);
        Task<InvoiceTotalModel> GetInvoiceTotal(MobileRequest request);
    }
}
