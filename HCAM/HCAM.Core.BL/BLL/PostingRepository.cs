using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class PostingRepository : IPostingCommand, IPostingQuery
    {
        private readonly ICommandRepository<Postings> _commandRepository;
        private readonly IQueryRepository<Postings> _queryRepository;

        public PostingRepository(ICommandRepository<Postings> repository, IQueryRepository<Postings> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        public Result<int> Save (PostingItem entity)
        {
            switch (entity.EntityState)
            {
                case ItemState.Added:
                    return _commandRepository.Add(entity.MapTo<Postings>()).MapResultTo<int, object>();
                case ItemState.Modified:
                    return _commandRepository.InlineUpdate(entity, entity.Id);
                case ItemState.Delete:
                    return _commandRepository.Delete(entity.Id);
                case ItemState.Unchanged:
                    return Result<int>.Fail<int>("Unchanged Object. Nothing to save");
                default:
                    return Result<int>.Fail<int>("Entity state unknown");
            }
        }

        public Result<IEnumerable<PostingItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<PostingItem, Postings>();

        public async Task<Result<IEnumerable<PostingItem>>> FindAsync (PostingFilter criteria)
        {
            var sql = @"SELECT P.*,
	                        M.MapperName AS CodeMapping
                        FROM dbo.[Postings] P
                            INNER JOIN dbo.[CodeMappers] M ON M.Id = P.CodeMappingId";
            var sqlWhere = " WHERE ";
            var sqlFilter = string.Empty;

            //if (criteria?. != null)
            //{
            //    sqlFilter += $" CA.Code = {criteria.ActionCode.Value}";
            //}

            if (sqlWhere.Trim() != "WHERE") sql += sqlWhere + sqlFilter;
            return await _queryRepository.ExecuteSqlQueryAsync(sql).TaskAsyncResponse<PostingItem, Postings>();
        }

        public Result<PostingItem> GetById (int id) => _queryRepository.GetById(id).MapResultTo<PostingItem, Postings>();
    }
}