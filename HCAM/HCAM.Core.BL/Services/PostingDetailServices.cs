using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.Services
{
    public class PostingDetailServices : IPostingDetailServices
    {
        private readonly IPostingDetailCommand _command;
        private readonly IPostingDetailQuery _query;
        private readonly ICodeMapperDetailQuery _queryCodeMapperDetailQuery;

        public PostingDetailServices(IPostingDetailCommand repository, IPostingDetailQuery query, ICodeMapperDetailQuery queryCodeMapperDetailQuery)
        {
            _command = repository;
            _query = query;
            _queryCodeMapperDetailQuery = queryCodeMapperDetailQuery;
        }

        public Result<int> Save (IEnumerable<PostingDetailItem> entities) =>  _command.Save(entities);

        public Task<Result<int>> SaveAsync (IEnumerable<PostingDetailItem> entities) => _command.SaveAsync(entities);
        public Result<IEnumerable<PostingDetailItem>> GetDetails (int postingId) => _query.GetDetails(postingId);

        public Result<IEnumerable<PostingDetailItem>> GenerateDetails(PostingItem posting) => 
                _queryCodeMapperDetailQuery.FindAsync().Result
                .Bind(result =>
                {
                    var newMappings = result.Select(item => new PostingDetailItem
                    {
                        PostingId = posting.Id,
                        ActionCodeId = item.ActionCodeId,
                        EffectiveDate = posting.EndDate,
                        EntityState = ItemState.Added
                    });
                    return Result<IEnumerable<PostingDetailItem>>.Ok(newMappings);
                })
                .Bind(s => Save(s))
                .Bind(x => GetDetails(posting.Id));

        public Result<PostingItem> AddItemDetails (PostingItem posting)
        {
            return GenerateDetails(posting).Bind(details =>
            {
                posting.DetailItems = details;
                return Result<PostingItem>.Ok(posting);
            });
        }
    }
}