using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common
{
    public interface IDbFactory
    {
        EURODbContext Init();
    }
}