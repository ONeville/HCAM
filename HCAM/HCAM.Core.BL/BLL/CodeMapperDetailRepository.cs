using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class CodeMapperDetailRepository : ICodeMapperDetailCommand, ICodeMapperDetailQuery
    {
        private readonly ICommandRepository<CodeMapperDetails> _commandRepository;
        private readonly IQueryRepository<CodeMapperDetails> _queryRepository;

        public CodeMapperDetailRepository(ICommandRepository<CodeMapperDetails> repository, IQueryRepository<CodeMapperDetails> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }
        private Result<int> DeleteDetail (CodeMapperDetailItem entity)
        {
            var sql = $@"DELETE FROM dbo.[CodeMapperDetails]
                         WHERE CodeMapperId = @CodeMapperId
                            AND ActionCodeId = @CodeMapperId;";
            return _commandRepository.ExecuteSqlNonQuery(sql, new { CodeMapperId = entity.CodeMapperId, ActionCodeId = entity.ActionCodeId });
        }
        private Result<int> UpdateDetail(CodeMapperDetailItem entity)
        {
            var sql = $@"UPDATE dbo.[CodeMapperDetails]
                            SET AccountingCode = @AccountingCode,
                                CreditAccountId = @CreditAccountId,
                                DebitAccountId = @DebitAccountId
                         WHERE CodeMapperId = @CodeMapperId
                            AND ActionCodeId = @CodeMapperId;";
            return _commandRepository.ExecuteSqlNonQuery(sql, new
            {
                CodeMapperId = entity.CodeMapperId,
                ActionCodeId = entity.ActionCodeId,
                AccountingCode = entity.AccountingCode,
                CreditAccountId = entity.CreditAccountId,
                DebitAccountId = entity.DebitAccountId 
            });
        }
        public async Task<Result<int>> Save (IEnumerable<CodeMapperDetailItem> entity)
        {
            var itemToUpdate = entity.Where(x => x.EntityState == ItemState.Modified).Select(UpdateDetail);
            var itemToDelete = entity.Where(x => x.EntityState == ItemState.Delete).Select(DeleteDetail);

            var items = entity.Where(x => x.EntityState == ItemState.Added).EnumerableTo<CodeMapperDetails>();
            var addItem = await _commandRepository.AddBulkAsync(items).TaskAsyncResponse<int, int>();

            var result = itemToUpdate.Count(x => x.Success) + itemToDelete.Count(x => x.Success);
            if (addItem.Success)
            {
                result += addItem.Value;
                return Result<int>.Ok(result);
            }
            var errMsg = addItem.Error + 
                string.Join("\n -", itemToUpdate.Select(err => err.Error)
                .Union(itemToDelete.Select(err => err.Error)));

            return Result<int>.Fail<int>(errMsg);
        }

        public Result<IEnumerable<CodeMapperDetailItem>> Find (object criteria = null) => _queryRepository.Get(criteria).MapResultsTo<CodeMapperDetailItem, CodeMapperDetails>();

        public Task<Result<IEnumerable<CodeMapperDetailItem>>> FindAsync () => _queryRepository.GetAsync().TaskAsyncResponse<CodeMapperDetailItem, CodeMapperDetails>();

        public Result<IEnumerable<CodeMapperDetailItem>> GetDetails (int mapperId)
        {
            var sql = $@"SELECT
	                        CM.*
                          , CA.AccountName AS CreditAccountName
                          , DA.AccountName AS DebitAccountName
                          , AC.Code AS ActionCode
                          , AC.Name AS ActionCodeName
                        FROM dbo.[CodeMapperDetails] CM
	                        INNER JOIN dbo.[ActionCodes] AC ON AC.Id = CM.ActionCodeId
	                        LEFT JOIN dbo.[Accounts] CA ON CA.Id = CM.CreditAccountId
	                        LEFT JOIN dbo.[Accounts] DA ON DA.Id = CM.DebitAccountId
                        WHERE CM.CodeMapperId = { mapperId }";
            return _queryRepository.ExecuteSqlQuery(sql).MapResultsTo<CodeMapperDetailItem, CodeMapperDetails>();
        }
        public Result<IEnumerable<CodeMapperDetailItem>> GenerateDetails (int mapperId)
        {
            var actionCodes = _queryRepository.Get();
            if (actionCodes.Failure)
                Result<IEnumerable<CodeMapperDetailItem>>.Fail<IEnumerable<CodeMapperDetailItem>>(actionCodes.Error);
            var itemDetails = actionCodes.Value.Select(item => new CodeMapperDetailItem
            {
                ActionCodeId = item.Id,
                CodeMapperId = mapperId,
                AccountingCode = $"000{item.Id}",
                EntityState = ItemState.Added
            });


            var result = Save(itemDetails);
            if (result.IsFaulted) return Result<IEnumerable<CodeMapperDetailItem>>.Fail<IEnumerable<CodeMapperDetailItem>>(result.Result.Error);

            return GetDetails(mapperId);
        }
    }
}