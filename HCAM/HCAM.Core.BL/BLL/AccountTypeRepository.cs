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
    public class AccountTypeRepository : IAccountTypeCommand, IAccountTypeQuery
    {
        private readonly ICommandRepository<AccountTypes> _commandRepository;
        private readonly IQueryRepository<AccountTypes> _queryRepository;

        public AccountTypeRepository(ICommandRepository<AccountTypes> repository, IQueryRepository<AccountTypes> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        public Result<int> Add(AccountTypeItem entity) => _commandRepository.Add(entity.MapTo<AccountTypes>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<AccountTypeItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<AccountTypes>(), id).MapResultTo<AccountTypeItem, AccountTypes>();
        public Result<AccountTypeItem> GetById(int id) => _queryRepository.GetById(id).MapResultTo<AccountTypeItem, AccountTypes>();
        public Result<IEnumerable<AccountTypeItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<AccountTypeItem, AccountTypes>();
        public async Task<Result<IEnumerable<AccountTypeItem>>> FindAsync(object criteria) => await _queryRepository.GetAsync(criteria).TaskAsyncResponse<AccountTypeItem, AccountTypes>();
    }
}
