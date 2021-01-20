using System;
using System.Collections.Generic;
using System.Text;

namespace EuroMobileApp.Services.Interfaces
{
    public interface IUser
    {
         string FullName { get; set; }
         string Password { get; set; }
         string Username { get; set; }
         string Email { get; set; }
         long UserId { get; set; }
         int UserTypeId { get; set; }
         bool Inactive { get; set; }
         DateTime CreatedDate { get; set; }
         string status { get; set; }

    }
}
