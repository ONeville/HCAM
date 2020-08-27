using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IPostingDetailCommand
    {
        Result<int> Save(IEnumerable<PostingDetailItem> entities);
        Task<Result<int>> SaveAsync(IEnumerable<PostingDetailItem> entities);
    }
    public interface IPostingDetailQuery
    {
        Result<IEnumerable<PostingDetailItem>> GetDetails(int postingId);
    }
}