using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers.Configs
{
    [Produces("application/json")]
    [Route("api/ActionCodes")]
    public class ActionCodesController : Controller
    {
        private readonly IActionCodeServices _services;
        private readonly IActionCodeCommand _repositoryCommand;
        private readonly IActionCodeQuery _repositoryQuery;
        public ActionCodesController(IActionCodeServices services, IActionCodeCommand repositoryCommand, IActionCodeQuery repositoryQuery)
        {
            _services = services;
            _repositoryCommand = repositoryCommand;
            _repositoryQuery = repositoryQuery;
        }
        // GET: api/AccountTypes
        [HttpGet]
        public Result<IEnumerable<ActionCodeItem>> Get()
        {
            var items = _repositoryQuery.Find();
            return items;
        }

        // GET: api/AccountTypes/5
        [HttpGet("{id}", Name = "GetActionCode")]
        public Result<ActionCodeItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/AccountTypes
        [HttpPost]
        public Result<int> Post([FromBody]ActionCodeItem value)
        {
            return _services.Add(value);
        }

        // PUT: api/AccountTypes/5
        [HttpPut("{id}")]
        public Result<ActionCodeItem> Put(int id, [FromBody]ActionCodeItem value)
        {
            return _repositoryCommand.Update(id, value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Result<int> Delete(int id)
        {
            return _repositoryCommand.Delete(id);
        }
    }
}