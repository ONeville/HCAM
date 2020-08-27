using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers.Configs
{
    [Produces("application/json")]
    [Route("api/CodeMappers")]
    public class CodeMappersController : Controller
    {
        private readonly ICodeMapperServices _services;
        private readonly IReadCodeMapperServices _serviceRead;
        public CodeMappersController(ICodeMapperServices services, IReadCodeMapperServices serviceRead)
        {
            _services = services;
            _serviceRead = serviceRead;
        }
        // GET: api/CodeMappers
        [HttpGet]
        public Result<IEnumerable<CodeMapperItem>> Get()
        {
            return _serviceRead.Find(); 
        }
        
        // GET: api/CodeMappers/5
        [HttpGet("{id}")]
        public Result<CodeMapperItem> Get(int? id)
        {
            var item = id.HasValue ? _serviceRead.GetById(id.Value) : _serviceRead.GetDefault();
            return item;
        }
        
        // POST: api/CodeMappers
        [HttpPost]
        public Result<int> Post([FromBody]CodeMapperItem entity)
        {
            entity.EntityState = ItemState.Added;
           return _services.Save(entity);
        }
        
        // PUT: api/CodeMappers/5
        [HttpPut("{id}")]
        public Result<int> Put(int id, [FromBody]CodeMapperItem entity)
        {
            entity.EntityState = ItemState.Modified;
            return _services.Save(entity);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Result<int> Delete(int id)
        {
            return _services.Save(new CodeMapperItem{ Id = id, EntityState = ItemState.Delete });
        }
    }
}