using System;
using System.Collections.Generic;
using System.Text;
using HCAM.RepoDb.Dal.Interfaces;
using HCAM.RepoDb.Dal.Models;
using HCAM.RepoDb.Dal.Repositories;
using RepoDb;

namespace HCAM.RepoDb.Dal.IoC
{
    public static class DalIoC
    {
        public static Dictionary<Type, Type> RegisterDalTypes ()
        {
            return new Dictionary<Type, Type>
            {
                {  typeof(IDbContext), typeof(DbContext) },
                {  typeof(ICommandRepository<AccountTypes>), typeof(CommandRepository<AccountTypes>) },
                {  typeof(IQueryRepository<AccountTypes>), typeof(QueryRepository<AccountTypes>) },

                {  typeof(ICommandRepository<Accounts>), typeof(CommandRepository<Accounts>) },
                {  typeof(IQueryRepository<Accounts>), typeof(QueryRepository<Accounts>) },

                {  typeof(ICommandRepository<ActionCodes>), typeof(CommandRepository<ActionCodes>) },
                {  typeof(IQueryRepository<ActionCodes>), typeof(QueryRepository<ActionCodes>) },

                {  typeof(ICommandRepository<CodeMapperDetails>), typeof(CommandRepository<CodeMapperDetails>) },
                {  typeof(IQueryRepository<CodeMapperDetails>), typeof(QueryRepository<CodeMapperDetails>) },

                {  typeof(ICommandRepository<CodeMappers>), typeof(CommandRepository<CodeMappers>) },
                {  typeof(IQueryRepository<CodeMappers>), typeof(QueryRepository<CodeMappers>) },

                {  typeof(ICommandRepository<Entries>), typeof(CommandRepository<Entries>) },
                {  typeof(IQueryRepository<Entries>), typeof(QueryRepository<Entries>) },

                {  typeof(ICommandRepository<JournalEntries>), typeof(CommandRepository<JournalEntries>) },
                {  typeof(IQueryRepository<JournalEntries>), typeof(QueryRepository<JournalEntries>) },

                {  typeof(ICommandRepository<PostingDetails>), typeof(CommandRepository<PostingDetails>) },
                {  typeof(IQueryRepository<PostingDetails>), typeof(QueryRepository<PostingDetails>) },

                {  typeof(ICommandRepository<Postings>), typeof(CommandRepository<Postings>) },
                {  typeof(IQueryRepository<Postings>), typeof(QueryRepository<Postings>) },

                {  typeof(ICommandRepository<Users>), typeof(CommandRepository<Users>) },
                {  typeof(IQueryRepository<Users>), typeof(QueryRepository<Users>) }
            };
        }
    }
}
