using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class EntryRepository : IEntryCommand, IEntryQuery
    {
        private readonly ICommandRepository<Entries> _commandRepository;
        private readonly IQueryRepository<Entries> _queryRepository;

        public EntryRepository(ICommandRepository<Entries> repository, IQueryRepository<Entries> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        public Result<int> Add(EntryItem entity) => _commandRepository.Add(entity.MapTo<Entries>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<EntryItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<Entries>(), id).MapResultTo<EntryItem, Entries>();
        public Result<EntryItem> GetById(int id) => _queryRepository.GetById(id).MapResultTo<EntryItem, Entries>();
        public Result<IEnumerable<EntryItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<EntryItem, Entries>();
        public async Task<Result<IEnumerable<EntryItem>>> FindAsync(object criteria) => await _queryRepository.GetAsync().TaskAsyncResponse<EntryItem, Entries>();
    }
}