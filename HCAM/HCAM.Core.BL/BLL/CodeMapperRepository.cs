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
    public class CodeMapperRepository : ICodeMapperCommand, ICodeMapperQuery
    {
        private readonly ICommandRepository<CodeMappers> _commandRepository;
        private readonly IQueryRepository<CodeMappers> _queryRepository;

        public CodeMapperRepository(ICommandRepository<CodeMappers> repository, IQueryRepository<CodeMappers> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        private Result<int> ResetDefault()
        {
            var sql = $@"UPDATE dbo.[CodeMappers]
                         SET IsDefault = 0";
            return _commandRepository.ExecuteSqlNonQuery(sql, null);
        }
        public Result<int> Save (CodeMapperItem entity)
        {
            switch (entity.EntityState)
            {
                case ItemState.Added:
                    return _commandRepository.Add(entity.MapTo<CodeMappers>()).MapResultTo<int, object>();
                case ItemState.Modified:
                    if (!entity.IsDefault) return _commandRepository.InlineUpdate(entity, entity.Id);
                    return ResetDefault().Bind<int>(x => _commandRepository.InlineUpdate(entity, entity.Id));
                case ItemState.Delete:
                    return _commandRepository.Delete(entity.Id);
                case ItemState.Unchanged:
                    return Result<int>.Fail<int>("Unchanged Object. Nothing to save");
                default:
                    return Result<int>.Fail<int>("Entity state unknown");
            }
        }

        public Result<IEnumerable<CodeMapperItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<CodeMapperItem, CodeMappers>();

        public async Task<Result<IEnumerable<CodeMapperItem>>> FindAsync (object filter = null) => await _queryRepository.GetAsync(filter).TaskAsyncResponse<CodeMapperItem, CodeMappers>();
        public Result<CodeMapperItem> GetById(int id) => _queryRepository.GetById(id).MapResultTo<CodeMapperItem, CodeMappers>();
    }
}