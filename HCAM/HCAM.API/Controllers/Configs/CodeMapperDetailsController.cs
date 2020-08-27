using System.Collections.Generic;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers.Configs
{
    [Produces("application/json")]
    [Route("api/CodeMapperDetails")]
    public class CodeMapperDetailsController : Controller
    {
        private readonly ICodeMapperDetailServices _services;
        public CodeMapperDetailsController(ICodeMapperDetailServices services)
        {
            _services = services;
        }

        // GET: api/CodeMappers/Generates/5
        [HttpGet("{id}")]
        public Result<IEnumerable<CodeMapperDetailItem>> Get(int mapperId)
        {
            return _services.GetDetails(mapperId);
        }

        // POST: api/CodeMappers
        [HttpPost("{items}")]
        public Result<int> Post(IEnumerable<CodeMapperDetailItem> items)
        {
            return _services.Save(items);
        }

        // PUT: api/CodeMappers/5
        [HttpPut("{mapperId}")]
        public Result<IEnumerable<CodeMapperDetailItem>> Put(int mapperId)
        {
            return _services.GenerateDetails(mapperId);
        }

        // PUT: api/CodeMappers/5
        [HttpPut("{id}")]
        public IEnumerable<CodeMapperDetailItem> Put(int id, [FromBody]IEnumerable<CodeMapperDetailItem> items)
        {
            _services.Save(items);
            //return _services.GenerateDetails(mapperId);
            return null;
        }
    }
}