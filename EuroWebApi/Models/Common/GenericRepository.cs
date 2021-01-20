using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EuroWebApi.Models.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private EURODbContext dbContext;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }
        protected EURODbContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }
        public GenericRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
        }
        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }
    }
}