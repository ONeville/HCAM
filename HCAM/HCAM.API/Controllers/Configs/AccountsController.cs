using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers.Configs
{
    [Produces("application/json")]
    [Route("api/Accounts")]
    public class AccountsController : Controller
    {
        private readonly IAccountCommand _repositoryCommand;
        private readonly IAccountQuery _repositoryQuery;
        public AccountsController(IAccountCommand repositoryCommand, IAccountQuery repositoryQuery)
        {
            _repositoryCommand = repositoryCommand;
            _repositoryQuery = repositoryQuery;
        }

        // GET: api/Accounts
        [HttpGet]
        public Result<IEnumerable<AccountItem>> Get()
        {
            //var items = _repositoryQuery.Find();
            var items = _repositoryQuery.GetDetails(new AccountFilter());
            return items;
        }

        // GET: api/Accounts
        //[HttpGet(Name = "GetAsync")]
        //public async Task<Result<IEnumerable<AccountItem>>> GetAsync()
        //{
        //    var items= _repositoryQuery.FindAsync();
        //    return await items;
        //}

        // GET: api/Accounts/5
        [HttpGet("{id}", Name = "GetAccount")]
        public Result<AccountItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/Accounts
        [HttpPost]
        public Result<int> Post([FromBody]AccountItem value)
        {
            return _repositoryCommand.Add(value);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id}")]
        public Result<AccountItem> Put(int id, [FromBody]AccountItem value)
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
