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
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly IUserCommand _repositoryCommand;
        private readonly IUserQuery _repositoryQuery;
        public UsersController(IUserQuery repositoryQuery, IUserCommand repositoryCommand)
        {
            _repositoryQuery = repositoryQuery;
            _repositoryCommand = repositoryCommand;
        }
        // GET: api/Users
        [HttpGet]
        public Result<IEnumerable<UserItem>> Get()
        {
            return _repositoryQuery.Find();
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUser")]
        public Result<UserItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/Users
        [HttpPost]
        public Result<int> Post([FromBody]UserItem value)
        {
            return _repositoryCommand.Add(value);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public Result<UserItem> Put(int id, [FromBody]UserItem value)
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
