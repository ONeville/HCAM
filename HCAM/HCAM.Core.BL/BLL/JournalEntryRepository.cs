using System.Collections.Generic;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Models;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.BLL
{
    public class JournalEntryRepository : IJournalEntryCommand, IJournalEntryQuery
    {
        private readonly ICommandRepository<JournalEntries> _commandRepository;
        private readonly IQueryRepository<JournalEntries> _queryRepository;

        public JournalEntryRepository(ICommandRepository<JournalEntries> repository, IQueryRepository<JournalEntries> queryRepository)
        {
            _queryRepository = queryRepository;
            _commandRepository = repository;
        }
        public Result<int> Add(JournalEntryItem entity) => _commandRepository.Add(entity.MapTo<JournalEntries>()).MapResultTo<int, object>();
        public Result<int> Delete(int id) => _commandRepository.Delete(id);
        public Result<JournalEntryItem> Update(int id, object entity) => _commandRepository.Update(entity.MapTo<JournalEntries>(), id).MapResultTo<JournalEntryItem, JournalEntries>();
        public Result<IEnumerable<JournalEntryItem>> Find (JournalEntryFilter criteria)
        {
            var sql = @"SELECT JE.*, E.EntryDescription, 
                            U.UserName AS ProcessBy,
                            DA.AccountName AS DebitAccount,
                            CA.AccountName AS CreditAccount 
                        FROM dbo.[JournalEntries] JE
                            INNER JOIN dbo.[Entries] E ON E.Id = JE.EntryId
                            INNER JOIN dbo.[Users] U ON U.Id = JE.UserId
                            INNER JOIN dbo.[Accounts] DA ON DA.Id = JE.DebitAccountId
                            INNER JOIN dbo.[Accounts] CA ON CA.Id = JE.CreditAccountId";
            var sqlWhere = " WHERE ";
            if (criteria?.Id != null)
            {
                sqlWhere += $" JE.Id = {criteria.Id.Value}";
            }
            if (criteria?.CreditAccountId != null)
            {
                sqlWhere += $" CA.Id = {criteria.CreditAccountId.Value}";
            }
            if (criteria?.DebitAccountId != null)
            {
                sqlWhere += $" DA.Id = {criteria.DebitAccountId.Value}";
            }

            if (sqlWhere.Trim() != "WHERE") sql += sqlWhere;

            return _queryRepository.ExecuteSqlQuery(sql).MapResultsTo<JournalEntryItem, JournalEntries>();
        }
        public Result<JournalEntryItem> GetById (int id) => _queryRepository.GetById(id).MapResultTo<JournalEntryItem, JournalEntries>();
    }
}