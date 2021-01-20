using EuroWebApi.ViewModels;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using EuroWebApi.Services.Interfaces;
using EuroWebApi.Models;
using EuroWebApi.Services.Implements;

namespace EuroWebApi.Controllers
{
    public class EuroApiController : Controller
    {
        private readonly IEuroService _euroService;

        public EuroApiController()
        {
            _euroService = new EuroService();

        }

        [ValidateInput(false)]
        public JsonResult Authenticate(LoginRequest loginRequest)
        {
            var response = _euroService.Authenticate(loginRequest);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        //[HttpPost]
        //public IHttpActionResult Login(LoginRequest loginRequest)
        //{
        //    var response = _euroService.Authenticate(loginRequest);
        //    if (response.Success)
        //    {
        //        return Ok(response);
        //    }
        //    return Content(HttpStatusCode.BadRequest, response);
        //}




        [ValidateInput(false)]
        public JsonResult Login(string userName, string password)
        {
            LoginViewModel vm = new LoginViewModel();
            bool status = false;
            string message = "";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_CheckLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = userName;
                        cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                        int userExist = 0;

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {

                            vm.status = r[0].ToString();
                            vm.UserId = Convert.ToInt32(r[1].ToString());
                            vm.LoginName = r[2].ToString();
                            vm.UserFullName = r[3].ToString();
                            vm.UserTypeId = Convert.ToInt32(r[4].ToString());

                            if (vm.LoginName.ToLower() == "fail")
                            {
                                userExist = 0;
                            }
                            else
                            {
                                userExist = 1;
                            }
                        }
                        r.Close();
                        if (userExist > 0)
                        {
                            status = true;
                            message = "";
                        }
                        else
                        {
                            status = false;
                            message = "Invalid User";
                        }
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            var data = vm;
            return Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult GetOrdersByUserId(int UserId)
        {
            List<OrdersViewModel> vm = new List<OrdersViewModel>();
            bool status = false;
            string message = "No orders found";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_GetOrdersByUserID", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            OrdersViewModel order = new OrdersViewModel();

                            order.UserId = Convert.ToInt32(r[0]);
                            order.WorkOrderId = Convert.ToInt32(r[1].ToString());
                            order.WorkOrderDate = Convert.ToDateTime(r[2]);
                            order.CustomerName = r[3].ToString();
                            order.ApplianceType = r[4].ToString();
                            order.JobStatus = r[5].ToString();

                            vm.Add(order);

                            status = true;
                            message = "";
                        }
                        r.Close();

                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            var data = vm;
            return Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult GetCustomerAppliancesByWorkOrderId(int WorkOrderId)
        {
            List<ApplianceViewModel> vm = new List<ApplianceViewModel>();
            bool status = false;
            string message = "No orders found";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_webapi_GetCustomerAppliances", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@WOrkOrderId", SqlDbType.BigInt).Value = WorkOrderId;

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ApplianceViewModel appliance = new ApplianceViewModel();

                            appliance.ApplianceName = r[0].ToString();
                            appliance.ManufacturerName = r[1].ToString();
                            appliance.SerialNumber = r[2].ToString();
                            appliance.ModelNumber = r[3].ToString();
                            appliance.ModelImage = r[4].ToString();
                            appliance.CustomerId = Convert.ToInt32(r[5].ToString());
                            appliance.CustomerName = r[6].ToString();
                            appliance.CustomerApplianceId = Convert.ToInt32(r[7].ToString());
                            appliance.WorkorderId = Convert.ToInt32(r[8].ToString());
                            appliance.ApplianceTypeId = Convert.ToInt32(r[9].ToString());
                            appliance.ManufacturerId = Convert.ToInt32(r[10].ToString());

                            vm.Add(appliance);

                            status = true;
                            message = "";
                        }
                        r.Close();
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            var data = vm;
            return Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult GetApplianceTypes()
        {
            List<ApplianceViewModel> vm = new List<ApplianceViewModel>();
            bool status = false;
            string message = "No appliance types found";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_GetApplianceTypes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ApplianceViewModel appliance = new ApplianceViewModel();

                            appliance.ApplianceTypeId = Convert.ToInt32(r[0].ToString());
                            appliance.ApplianceName = r[1].ToString();

                            vm.Add(appliance);

                            status = true;
                            message = "";
                        }
                        r.Close();
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            var data = vm;
            return Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public JsonResult GetManufacturers()
        {
            List<ManufacturerViewModel> vm = new List<ManufacturerViewModel>();
            bool status = false;
            string message = "No manufacturer found";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_GetManufactures", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ManufacturerViewModel manufacturer = new ManufacturerViewModel();

                            manufacturer.ManufacturerId = Convert.ToInt32(r[0].ToString());
                            manufacturer.ManufacturerName = r[1].ToString();

                            vm.Add(manufacturer);

                            status = true;
                            message = "";
                        }
                        r.Close();
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            var data = vm;
            return Json(new { status = status, message = message, data = data }, JsonRequestBehavior.AllowGet);
        }



        [ValidateInput(false)]
        public JsonResult UpdateAppliance(ApplianceViewModel appliance) // To be Modified
        {
            bool status = false;
            string message = "An error occurred while updating the record.";

            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connection))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_update_appliance", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@WorkorderId", SqlDbType.Int).Value = appliance.WorkorderId;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = appliance.UserId;
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = appliance.CustomerName;
                        cmd.Parameters.Add("@CustomerApplianceId", SqlDbType.Int).Value = appliance.CustomerApplianceId;
                        cmd.Parameters.Add("@ApplianceTypeId", SqlDbType.Int).Value = appliance.ApplianceTypeId;
                        cmd.Parameters.Add("@ManufacturerId", SqlDbType.Int).Value = appliance.ManufacturerId;
                        cmd.Parameters.Add("@SerialNumber", SqlDbType.VarChar).Value = appliance.SerialNumber;
                        cmd.Parameters.Add("@ModelNumber", SqlDbType.VarChar).Value = appliance.ModelNumber;
                        cmd.Parameters.Add("@ModelImage", SqlDbType.VarChar).Value = appliance.ModelImage;

                        cmd.ExecuteNonQuery();
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            message = r[0].ToString();
                        }
                        r.Close();
                        if (message.ToLower() == "updatedsuccessfully")
                        {
                            status = true;
                            message = "Record updated successfully.";
                        }
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
                catch (Exception ex)
                {
                    status = false;
                    message = ex.Message;
                }
            }

            return Json(new { status = status, message = message }, JsonRequestBehavior.AllowGet);
        }

        #region Upload Image
        [ValidateInput(false)]
        public JsonResult UploadApplianceImage(UploadImageViewModel imgDetails)
        {
            bool isSaved = false;
            string message = "";
            try
            {
                string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ADO"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connection))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_webapi_Upload_WorkOrderImage", con))
                    {
                        string fileName = imgDetails.imgName;
                        //Set the Image File Path.
                        string filePath = "~/Uploads/Images/" + fileName;
                        System.IO.File.WriteAllBytes(Server.MapPath(filePath), imgDetails.image);

                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.Add("@WorkOrderId", SqlDbType.Int).Value = imgDetails.workOrderId;
                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = imgDetails.userId;
                        cmd.Parameters.Add("@CustomerName", SqlDbType.VarChar).Value = imgDetails.customerName;
                        cmd.Parameters.Add("@FileName", SqlDbType.VarChar).Value = imgDetails.imgName;
                        cmd.Parameters.Add("@Notes", SqlDbType.VarChar).Value = imgDetails.notes;

                        cmd.ExecuteNonQuery();

                        isSaved = true;
                        cmd.Dispose();
                    }
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                isSaved = false;
                message = ex.StackTrace;
            }


            return Json(new { status = isSaved, message = message }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
