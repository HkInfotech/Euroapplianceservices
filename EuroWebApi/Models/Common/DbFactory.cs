using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common
{
    public class DbFactory : IDbFactory
    {
        EURODbContext dbContext;
        public EURODbContext Init()
        {
            return dbContext ?? (dbContext = new EURODbContext());
        }
    }
}