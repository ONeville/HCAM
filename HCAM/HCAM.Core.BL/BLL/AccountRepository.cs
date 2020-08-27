using System.Collections.Generic;
using System.Linq;
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
    public class AccountRepository : IAccountCommand, IAccountQuery
    {
        private readonly ICommandRepository<Accounts> _commandRepository;
        private readonly IQueryRepository<Accounts> _queryRepository;

        public AccountRepository(ICommandRepository<Accounts> repository, IQueryRepository<Accounts> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }
        
        public Result<int> Add(AccountItem entity) => _commandRepository.Add(entity.MapTo<Accounts>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<AccountItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<Accounts>(), id).MapResultTo<AccountItem, Accounts>();
        public async Task<Result<IEnumerable<AccountItem>>> FindAsync (object criteria) => await _queryRepository.GetAsync().TaskAsyncResponse<AccountItem, Accounts>();
        public Result<IEnumerable<AccountItem>> Find(object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<AccountItem, Accounts>();
        public Result<AccountItem> GetById (int id) => _queryRepository.GetById(id).MapResultTo<AccountItem, Accounts>();
        public Result<IEnumerable<AccountItem>> GetDetails(AccountFilter criteria)
        {
            var sql = @"SELECT A.*, AT.AccountTypeName 
                    FROM [Accounts] A 
                    INNER JOIN [AccountTypes] AT ON AT.Id = A.AccountTypeId";
            var sqlWhere = " WHERE ";
            if (criteria?.Id != null)
            {
                sqlWhere += $" A.Id = {criteria.Id.Value}";
            }
            if (criteria?.AccountTypeId != null)
            {
                sqlWhere += $" AT.Id = {criteria.AccountTypeId.Value}";
            }

            if (sqlWhere.Trim() != "WHERE") sql += sqlWhere;

            var result = _queryRepository.ExecuteSqlQuery(sql);

            return result.MapResultsTo<AccountItem, Accounts>();
        }
    }
}