using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Enums;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class ActionCodeRepository : IActionCodeCommand, IActionCodeQuery
    {
        private readonly ICommandRepository<ActionCodes> _commandRepository;
        private readonly IQueryRepository<ActionCodes> _queryRepository;

        public ActionCodeRepository(ICommandRepository<ActionCodes> repository, IQueryRepository<ActionCodes> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }

        public Result<int> Add(ActionCodeItem entity) => _commandRepository.Add(entity.MapTo<ActionCodes>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<ActionCodeItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<ActionCodes>(), id).MapResultTo<ActionCodeItem, ActionCodes>();
        public Result<IEnumerable<ActionCodeItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<ActionCodeItem, ActionCodes>();

        public async Task<Result<IEnumerable<ActionCodeItem>>> FindAsync(object criteria) => await _queryRepository.GetAsync().TaskAsyncResponse<ActionCodeItem, ActionCodes>();

        public Result<ActionCodeItem> GetById(int id) => _queryRepository.GetById(id).MapResultTo<ActionCodeItem, ActionCodes>();
        public Result<long> GetLastCode (int code)
        {
            var value1 = code.ToString();
            var value2 = code.ToString().ToCharArray()[0] + "9999";
            var filter = new {Field = "Code", Values = new[] {value1, value2}, Operation = OperationFilter.Between};
            return _queryRepository.CountRecord(filter);
        }
    }
}