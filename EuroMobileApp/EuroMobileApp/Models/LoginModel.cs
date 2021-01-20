using EuroMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class LoginModel: BaseVM
    {
        public bool status { get; set; }
        public string message { get; set; }
        public clsLogin data { get; set; }

        public LoginModel()
        {
            status = false;
            message = "";
            data = new clsLogin();
        }
    }
}
