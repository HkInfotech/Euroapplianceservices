

using EuroWebApi.Models;
using EuroWebApi.Models.Common;
using EuroWebApi.Models.Common.Request;
using EuroWebApi.Models.Common.Response;
using EuroWebApi.Models.ModelDTO;
using EuroWebApi.Services;
using EuroWebApi.Services.Implements;
using EuroWebApi.Services.Interfaces;
using EuroWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EuroWebApi.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/EuroApiV1")]
    public class EuroApiV1Controller : ApiController
    {
        private readonly IEuroService _euroService;
        public EuroApiV1Controller()
        {
            _euroService = new EuroService();
        }


        [HttpPost]
        [Route("Authenticate")]
        public IHttpActionResult Authenticate(LoginRequest loginRequest)
        {
            var response = _euroService.Authenticate(loginRequest);
            if (!response.Success)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, response);
            }
            else
            {
                return Ok(response);
            }

        }

        [Route("GetOrdersByUserId")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetOrdersByUserId(WorkOrderFilterRequest request)
        {
            Response<List<OrdersViewModel>> response = new Response<List<OrdersViewModel>>();
            try
            {
                response = _euroService.GetOrdersByUserId(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetCustomerAppliancesByWorkOrderId")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerAppliancesByWorkOrderId(MobileRequest request)
        {
            Response<List<ApplianceViewModel>> response = new Response<List<ApplianceViewModel>>();
            try
            {
                response = _euroService.GetCustomerAppliancesByWorkOrderId(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetApplianceTypes")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetApplianceTypes(MobileRequest request)
        {
            Response<List<ApplianceTypeViewModel>> response = new Response<List<ApplianceTypeViewModel>>();
            try
            {
                response = _euroService.GetApplianceTypes(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetManufacturers")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetManufacturers(MobileRequest request)
        {
            Response<List<ManufacturerViewModel>> response = new Response<List<ManufacturerViewModel>>();
            try
            {
                response = _euroService.GetManufacturers(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetJobNatures")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetJobNatures(MobileRequest request)
        {
            Response<List<JobNatureViewModel>> response = new Response<List<JobNatureViewModel>>();
            try
            {
                response = _euroService.GetJobNatures(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetJobStatus")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetJobStatus(MobileRequest request)
        {
            Response<List<JobStatusViewModel>> response = new Response<List<JobStatusViewModel>>();
            try
            {
                response = _euroService.GetJobStatus(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetTechnicians")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetTechnicians(MobileRequest request)
        {
            Response<List<TechnicianViewModel>> response = new Response<List<TechnicianViewModel>>();
            try
            {
                response = _euroService.GetTechnicians(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetWorkOrderDetailById")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWorkOrderDetailById(MobileRequest request)
        {
            Response<WorkOrderDetailViewModel> response = new Response<WorkOrderDetailViewModel>();
            try
            {
                response = _euroService.GetWorkOrderDetailById(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetWorkOrderServicesbyId")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWorkOrderServicesbyId(MobileRequest request)
        {
            Response<List<WorkOrderServiceViewModel>> response = new Response<List<WorkOrderServiceViewModel>>();
            try
            {
                response = _euroService.GetWorkOrderServicesbyId(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetWorkOrderPartsById")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWorkOrderPartsById(MobileRequest request)
        {
            Response<List<WorkOrderPartViewModel>> response = new Response<List<WorkOrderPartViewModel>>();
            try
            {
                response = _euroService.GetWorkOrderPartsById(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetWorkOrderTechRemarks")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWorkOrderTechRemarks(MobileRequest request)
        {
            Response<WorkOrderTechMarkViewModel> response = new Response<WorkOrderTechMarkViewModel>();
            try
            {
                response = _euroService.GetWorkOrderTechRemarks(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("SaveWorkOrder")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SaveWorkOrder(WorkOrderRequestModel request)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response = _euroService.SaveWorkOrder(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("SaveWorkOrderPart")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SaveWorkOrderPart(WorkOrderPartViewModel request)
        {
            Response<SaveWorkOrderPartResponse> response = new Response<SaveWorkOrderPartResponse>();
            try
            {
                response = _euroService.SaveWorkOrderPart(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("DeleteWorkOrderPart")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> DeleteWorkOrderPart(WorkOrderPartViewModel request)
        {
            Response<SaveWorkOrderPartResponse> response = new Response<SaveWorkOrderPartResponse>();
            try
            {
                response = _euroService.DeleteWorkOrderPart(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("SaveSignature")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SaveSignature(SaveSignatureRequest request)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response = _euroService.SaveSignature(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetInvoiceText")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetInvoiceText(InvoiceTextRequest request)
        {
            Response<string> response = new Response<string>();
            try
            {
                response = _euroService.GetInvoiceText(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetInvoiceTotal")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetInvoiceTotal(MobileRequest request)
        {
            Response<InvoiceTotalViewModel> response = new Response<InvoiceTotalViewModel>();
            try
            {
                response = _euroService.GetInvoiceTotal(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("UpdateCustomerInfo")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> UpdateCustomerInfo(CustomerInfoRequest request)
        {
            Response<CustomerInfoViewModel> response = new Response<CustomerInfoViewModel>();
            try
            {
                response = _euroService.UpdateCustomerInfo(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Route("GetCustomerInfo")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetCustomerInfo(MobileRequest request)
        {
            Response<CustomerInfoViewModel> response = new Response<CustomerInfoViewModel>();
            try
            {
                response = _euroService.GetCustomerInfo(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetInvoiceSignatureInfo")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetInvoiceSignatureInfo(MobileRequest request)
        {
            Response<CustomerInvoiceSignatureViewModel> response = new Response<CustomerInvoiceSignatureViewModel>();
            try
            {
                response = _euroService.GetInvoiceSignatureInfo(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("SendCustomerInvoice")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> SendCustomerInvoice(WorkOrderViewModel request)
        {

            Response<ResponseModel> response = new Response<ResponseModel>() { Success = true };
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                ResponseModel responseModel = new ResponseModel();
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_webapi_GetCustomerDetailsById", con))
                    {
                        #region Customer Detail
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@CustomerId", SqlDbType.BigInt).Value = request.CustomerId;

                        CustomerViewModel customer = new CustomerViewModel();
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            customer.CustomerId = Convert.ToInt64(r[0].ToString());
                            customer.CustomerName = r[1].ToString();
                            customer.CustomerEmail = r[2].ToString();
                        }
                        r.Close();
                        cmd.Dispose();
                        #endregion

                        if (!string.IsNullOrEmpty(customer.CustomerEmail))
                        {
                            #region Generating PDF to send as attachment
                            var reportProcessor = new Telerik.Reporting.Processing.ReportProcessor();
                            var typeReportSource = new Telerik.Reporting.TypeReportSource();
                            // reportToExport is the Assembly Qualified Name of the report
                            typeReportSource.TypeName = typeof(EuroWebApi.Reports.MobileInvoice).AssemblyQualifiedName;
                            typeReportSource.Parameters.Add(new Telerik.Reporting.Parameter("WOrkOrderId", request.WorkOrderId.ToString()));
                            var deviceInfo = new System.Collections.Hashtable();
                            deviceInfo["JavaScript"] = "this.print({bUI: true, bSilent: false, bShrinkToFit: true});";

                            var result = reportProcessor.RenderReport("PDF", typeReportSource, deviceInfo);
                            string fileName = "Invoice." + result.Extension;
                            #endregion

                            #region Send Confirmation Email to Dealer
                            var mailMessage = new Message
                            {
                                To = customer.CustomerEmail,
                                Subject = "Customer Invoice",
                                Type = MessageType.CustomerInvoiceEmail,
                                AttachmentName = fileName,
                                Attachment = result.DocumentBytes
                            };
                            mailMessage.Data.Add("CUSTOMER_NAME", customer.CustomerName);
                            MailerService.Instance().Send(mailMessage);
                            #endregion

                            responseModel.IsError = false;
                            responseModel.Message = "An Invoice Email Send Successfully to customer.";
                            response.ResponseContent = responseModel;
                        }
                        else
                        {
                            responseModel.IsError = true;
                            responseModel.Message = "Email address not exist for customer.";
                            response.ResponseContent = responseModel;
                        }
                    }
                }
                catch (Exception ex)
                {
                    responseModel.IsError = true;
                    responseModel.Message = "Failed to send an invoice email to customer. Error: " + ex.Message;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
            return Ok(response);
        }

        [Route("GetWorkOrderImages")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> GetWorkOrderImages(MobileRequest request)
        {
            Response<List<WorkOrderImageViewModel>> response = new Response<List<WorkOrderImageViewModel>>();
            try
            {
                response = _euroService.GetWorkOrderImages(request);
                if (response.Success)
                {
                    return Ok(response);
                }
                else
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
