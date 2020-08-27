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
    public class UserRepository : IUserCommand, IUserQuery
    {
        private readonly ICommandRepository<Users> _commandRepository;
        private readonly IQueryRepository<Users> _queryRepository;

        public UserRepository(ICommandRepository<Users> repository, IQueryRepository<Users> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        public Result<int> Add(UserItem entity) => _commandRepository.Add(entity.MapTo<Users>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<UserItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<Users>(), id).MapResultTo<UserItem, Users>();
        public Result<UserItem> GetById(int id) => _queryRepository.GetById(id).MapResultTo<UserItem, Users>();
        public Result<IEnumerable<UserItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<UserItem, Users>();
        public async Task<Result<IEnumerable<UserItem>>> FindAsync(object criteria) => await _queryRepository.GetAsync().TaskAsyncResponse<UserItem, Users>();
    }
}