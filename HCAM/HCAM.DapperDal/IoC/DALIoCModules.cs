using System;
using System.Collections.Generic;
using HCAM.DapperDal.Interfaces;

namespace HCAM.DapperDal.IoC
{
    public static class DALIoCModules
    {
        public static Dictionary<Type, Type> RegisterTypes()
        {
            return new Dictionary<Type, Type>
            {
                {  typeof(IDbContext), typeof(DbContext) },
                {  typeof(IRepository), typeof(Repository.Repository) }
            }; 
        }
    }
}
