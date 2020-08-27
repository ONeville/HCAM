using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces.Services
{
    public interface IReadCodeMapperServices
    {
        Result<IEnumerable<CodeMapperItem>> Find();
        Task<Result<IEnumerable<CodeMapperItem>>> FindAsync();
        Result<CodeMapperItem> GetById(int id);
        Result<CodeMapperItem> GetDefault(bool withDetail = true);
    }
    public interface ICodeMapperServices
    {
        Result<int> Save(CodeMapperItem entity);
    }
    public interface ICodeMapperDetailServices
    {
        Result<IEnumerable<CodeMapperDetailItem>> GetDetails(int mapperId);
        Result<IEnumerable<CodeMapperDetailItem>> GenerateDetails(int mapperId);
        Result<int> Save(IEnumerable<CodeMapperDetailItem> entities);
    }
    public interface IPostingDetailServices
    {
        Result<int> Save(IEnumerable<PostingDetailItem> entities);
        Task<Result<int>> SaveAsync(IEnumerable<PostingDetailItem> entities);
        Result<IEnumerable<PostingDetailItem>> GetDetails(int postingId);
        Result<IEnumerable<PostingDetailItem>> GenerateDetails(PostingItem posting);
        Result<PostingItem> AddItemDetails(PostingItem posting);
    }
}