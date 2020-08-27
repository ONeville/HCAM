using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCAM.API.Controllers
{
    [Produces("application/json")]
    [Route("api/PostingDetails")]
    public class PostingDetailsController : Controller
    {
        private readonly IPostingDetailServices _services;
        public PostingDetailsController(IPostingDetailServices services)
        {
            _services = services;
        }
        // GET: api/CodeMappers/Generates/5
        [HttpGet("{postingId}")]
        public Result<IEnumerable<PostingDetailItem>> Get(int postingId)
        {
            return _services.GetDetails(postingId);
        }

        // POST: api/CodeMappers
        [HttpPost("{items}")]
        public Result<int> Post(IEnumerable<PostingDetailItem> items)
        {
            return _services.Save(items);
        }

        // PUT: api/CodeMappers/5
        //[HttpPut("{posting}")]
        [HttpPut]
        public Result<IEnumerable<PostingDetailItem>> Put([FromBody]PostingItem posting)
        {
            return _services.GenerateDetails(posting);
        }

        // PUT: api/CodeMappers/5
        [HttpPut("{id}")]
        public Result<int> Put(int id, [FromBody]IEnumerable<PostingDetailItem> items)
        {
            return _services.Save(items);
        }

    }
}