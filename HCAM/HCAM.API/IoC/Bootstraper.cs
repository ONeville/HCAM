using System;
using System.Collections.Generic;
using HCAM.Core.BL.IoC;
using HCAM.RepoDb.Dal.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace HCAM.API.IoC
{
    public static class Bootstraper
    {
        public static void Configure(IServiceCollection services)
        {
            Configure(services, DalIoC.RegisterDalTypes());
            Configure(services, BLIoCModules.RegisterTypes());
        }
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfiles(GetMapperProfiles());
            });
        }
        private static void Configure(IServiceCollection services, Dictionary<Type, Type> types)
        {
            foreach (var type in types)
                services.AddScoped(type.Key, type.Value);
        }
        private static void ConfigureWithTuple(IServiceCollection services, IEnumerable<Tuple<Type, Type>> types)
        {
            foreach (var type in types)
                services.AddScoped(type.Item1, type.Item2);
        }
        private static IEnumerable<Type> GetMapperProfiles()
        {
            return new List<Type> { typeof(MappingEntityProfile) }; 
        }
    }
}
