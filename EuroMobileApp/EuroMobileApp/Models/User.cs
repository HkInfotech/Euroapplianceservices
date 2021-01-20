using EuroMobileApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Models
{
    public class User : IUser
    {
        public string FullName { get; set; }
        public string Password {  get; set; }
        public string Username {  get; set; }
        public string Email {  get; set; }
        public long UserId { get; set; }
        public int UserTypeId { get; set; }
        public bool Inactive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string status { get; set; }

    }
}
