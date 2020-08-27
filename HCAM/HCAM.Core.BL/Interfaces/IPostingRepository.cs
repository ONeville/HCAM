using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IPostingCommand
    {
        Result<int> Save(PostingItem entity);
    }

    public interface IPostingQuery
    {
        Result<IEnumerable<PostingItem>> Find(object criteria = null);
        Task<Result<IEnumerable<PostingItem>>> FindAsync (PostingFilter criteria = null);
        Result<PostingItem> GetById(int id);
    }
}