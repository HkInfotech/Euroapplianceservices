using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroWebApi.Models.Common
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}
