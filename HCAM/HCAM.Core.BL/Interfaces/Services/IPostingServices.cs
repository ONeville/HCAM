using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces.Services
{
    public interface IPostingServices
    {
        Result<IEnumerable<PostingItem>> Find(PostingFilter criteria = null);
        Task<Result<IEnumerable<PostingItem>>> FindAsync(PostingFilter criteria = null);
        Result<PostingItem> GetById(int id);
        Result<PostingItem> Save(PostingItem entity);
        Result<PostingItem> New();
    }
}