using System.Collections.Generic;
using System.Linq;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers
{
    [Produces("application/json")]
    [Route("api/JournalEntries")]
    public class JournalEntriesController : Controller
    {
        private readonly IJournalEntryCommand _repositoryCommand;
        private readonly IJournalEntryQuery _repositoryQuery;
        public JournalEntriesController(IJournalEntryQuery repositoryQuery, IJournalEntryCommand repositoryCommand)
        {
            _repositoryQuery = repositoryQuery;
            _repositoryCommand = repositoryCommand;
        }
        // GET: api/JournalEntries
        [HttpGet]
        public Result<IEnumerable<JournalEntryItem>> Get()
        {
            var items = _repositoryQuery.Find();
            return items;
        }

        // GET: api/JournalEntries/5
        [HttpGet("{id}", Name = "GetJournalEntry")]
        public Result<JournalEntryItem> Get(int id)
        {
            return _repositoryQuery.GetById(id);
        }

        // POST: api/JournalEntries
        [HttpPost]
        public Result<int> Post([FromBody]JournalEntryItem value)
        {
           return _repositoryCommand.Add(value);
        }

        // PUT: api/JournalEntries/5
        [HttpPut("{id}")]
        public Result<JournalEntryItem> Put(int id, [FromBody]JournalEntryItem value)
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
