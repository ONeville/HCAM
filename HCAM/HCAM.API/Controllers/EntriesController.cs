using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Entries")]
    public class EntriesController : Controller
    {
        private readonly IEntryCommand _repositoryCommand;
        private readonly IEntryQuery _repositoryQuery;
        public EntriesController(IEntryQuery repositoryQuery, IEntryCommand repositoryCommand)
        {
            _repositoryQuery = repositoryQuery;
            _repositoryCommand = repositoryCommand;
        }
        // GET: api/Entries
        [HttpGet]
        public Result<IEnumerable<EntryItem>> Get()
        {
            var items= _repositoryQuery.Find();
            return items;
        }

        // GET: api/Entries/5
        [HttpGet("{id}", Name = "GetEntry")]
        public Result<EntryItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/Entries
        [HttpPost]
        public Result<int> Post([FromBody]EntryItem value)
        {
            return _repositoryCommand.Add(value);
        }

        // PUT: api/Entries/5
        [HttpPut("{id}")]
        public Result<EntryItem> Put(int id, [FromBody]EntryItem value)
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
