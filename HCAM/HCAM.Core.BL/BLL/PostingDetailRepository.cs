using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class PostingDetailRepository : IPostingDetailCommand, IPostingDetailQuery
    {
        private readonly ICommandRepository<PostingDetails> _commandRepository;
        private readonly IQueryRepository<PostingDetails> _queryRepository;

        public PostingDetailRepository(ICommandRepository<PostingDetails> repository, IQueryRepository<PostingDetails> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }
        private Result<int> DeleteDetail(PostingDetailItem entity) => _commandRepository.Delete(entity.Id);
        private Result<int> UpdateDetail(PostingDetailItem entity) => _commandRepository.InlineUpdate(entity, entity.Id);
        private Result<int> UpdateDetailX(PostingDetailItem entity)
        {
            Result<int> BuildtResult(Result<PostingDetails> result) => result.Failure ? Result<int>.Fail<int>(result.Error) : Result<int>.Ok(result.Value.Id);
            return BuildtResult(_commandRepository.Update(entity, entity.Id));
        }

        public Result<int> Save (IEnumerable<PostingDetailItem> entities)
        {
            var items = entities.Where(x => x.EntityState == ItemState.Added).EnumerableTo<PostingDetails>();
            var addItem = _commandRepository.AddBulk(items);

            return HandleSavingResponse(entities, addItem);
        }

        public async Task<Result<int>> SaveAsync(IEnumerable<PostingDetailItem> entities)
        {
            var items = entities.Where(x => x.EntityState == ItemState.Added).EnumerableTo<PostingDetails>();
            var addItem = await _commandRepository.AddBulkAsync(items).TaskAsyncResponse<int, int>();

            return HandleSavingResponse(entities, addItem);
        }

        private  Result<int> HandleSavingResponse (IEnumerable<PostingDetailItem> entity, Result<int> addItem)
        {
            var items = entity.Where(x => x.EntityState == ItemState.Modified)
                .Select(UpdateDetail).Union(entity.Where(x => x.EntityState == ItemState.Delete).Select(DeleteDetail));

            if (addItem.Success)
            {
                var result = addItem.Value + items.Count(x => x.Success);
                ;
                return Result<int>.Ok(result);
            }
            var errMsg = addItem.Error + string.Join("\n -", items.Select(err => err.Error));

            return Result<int>.Fail<int>(errMsg);
        }

        public Result<IEnumerable<PostingDetailItem>> GetDetails (int postingId)
        {
            var sql = $@"SELECT
	                       P.*
                          , AC.Code AS ActionCode
                          , AC.Name AS ActionCodeName
                          , AC.Frequency AS Frequency
                        FROM dbo.[PostingDetails] P
	                        INNER JOIN dbo.[ActionCodes] AC ON AC.Id = P.ActionCodeId
                        WHERE P.PostingId = { postingId }";
            return _queryRepository.ExecuteSqlQuery(sql).MapResultsTo<PostingDetailItem, PostingDetails>();
        }
    }
}