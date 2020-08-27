using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers.Configs
{
    [Produces("application/json")]
    [Route("api/AccountTypes")]
    public class AccountTypesController : Controller
    {
        private readonly IAccountTypeCommand _repositoryCommand;
        private readonly IAccountTypeQuery _repositoryQuery;
        public AccountTypesController(IAccountTypeCommand repositoryCommand, IAccountTypeQuery repositoryQuery)
        {
            _repositoryCommand = repositoryCommand;
            _repositoryQuery = repositoryQuery;
        }
        // GET: api/AccountTypes
        [HttpGet]
        public Result<IEnumerable<AccountTypeItem>> Get()
        {
            var items = _repositoryQuery.Find();
            return items;
        }

        // GET: api/AccountTypes/5
        [HttpGet("{id}", Name = "GetAccountType")]
        public Result<AccountTypeItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/AccountTypes
        [HttpPost]
        public Result<int> Post([FromBody]AccountTypeItem value)
        {
            return _repositoryCommand.Add(value);
        }

        // PUT: api/AccountTypes/5
        [HttpPut("{id}")]
        public Result<AccountTypeItem> Put(int id, [FromBody]AccountTypeItem value)
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
