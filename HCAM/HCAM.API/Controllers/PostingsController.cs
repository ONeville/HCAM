using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers
{
    [Produces("application/json")]
    [Route("api/Postings")]
    public class PostingsController : Controller
    {
        private readonly IPostingServices _services;
        public PostingsController(IPostingServices services)
        {
            _services = services;
        }
        // GET: api/JournalEntries
        [HttpGet]
        public Result<IEnumerable<PostingItem>> Get()
        {
            var items = _services.Find();
            return items;
        }

        // GET: api/JournalEntries/5
        [HttpGet("{id}", Name = "GetPosting")]
        public Result<PostingItem> Get(int id)
        {
            return id == 0? _services.New() : _services.GetById(id);
        }
        
        // POST: api/JournalEntries
        [HttpPost]
        public Result<PostingItem> Post([FromBody]PostingItem value)
        {
            return _services.Save(value);
        }

        // PUT: api/JournalEntries/5
        [HttpPut("{id}")]
        public Result<PostingItem> Put(int id, [FromBody]PostingItem value)
        {
            return _services.Save(value);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public Result<PostingItem> Delete(int id)
        {
            return _services.Save(new PostingItem { Id = id, EntityState = ItemState.Delete });
        }
    }
}