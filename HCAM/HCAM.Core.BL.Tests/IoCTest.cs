using System;
using System.Collections.Generic;
using System.Text;
using HCAM.Core.BL.IoC;
using HCAM.RepoDb.Dal.IoC;
using StructureMap;

namespace HCAM.Core.BL.Tests
{
    public class IoCTest
    {
        public IoCTest ()
        {
            var container = Container.For<IoCRegisterTest>();

            //var app = container.GetInstance<Application>();
            //app.Run();
        }
    }

    public class IoCRegisterTest : Registry
    {
        public IoCRegisterTest ()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            InitializeMapper();
            RegisterDalType();
            RegisterBlType();
        }

        public void InitializeMapper()
        {
            AutoMapper.Mapper.Initialize((cfg) =>
            {
                cfg.AddProfiles(GetMapperProfiles());
            });
        }
        private IEnumerable<Type> GetMapperProfiles()
        {
            return new List<Type> { typeof(MappingEntityProfile) };
        }
        private void RegisterDalType()
        {
            var dalTypes = DalIoC.RegisterDalTypes();
            foreach (var dt in dalTypes)
            {
                For(dt.Key).Use(dt.Value);
            }
        }

        private void RegisterBlType ()
        {
            var dalTypes = BLIoCModules.RegisterTypes();
            foreach (var dt in dalTypes)
            {
                For(dt.Key).Use(dt.Value);
            }
        }
    }
}
