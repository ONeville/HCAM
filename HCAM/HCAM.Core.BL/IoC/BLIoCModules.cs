using System;
using System.Collections.Generic;
using HCAM.Core.BL.BLL;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Services;

namespace HCAM.Core.BL.IoC
{
    public static class BLIoCModules
    {
        public static Dictionary<Type, Type> RegisterTypes()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IAccountCommand), typeof(AccountRepository)},
                { typeof(IAccountQuery), typeof(AccountRepository)},
                { typeof(IEntryCommand), typeof(EntryRepository)},
                { typeof(IEntryQuery), typeof(EntryRepository)},
                { typeof(IJournalEntryCommand), typeof(JournalEntryRepository)},
                { typeof(IJournalEntryQuery), typeof(JournalEntryRepository)},
                { typeof(IAccountTypeCommand), typeof(AccountTypeRepository)},
                { typeof(IAccountTypeQuery), typeof(AccountTypeRepository)},
                { typeof(IUserCommand), typeof(UserRepository)},
                { typeof(IUserQuery), typeof(UserRepository)},
                { typeof(IActionCodeCommand), typeof(ActionCodeRepository)},
                { typeof(IActionCodeQuery), typeof(ActionCodeRepository)},
                { typeof(IActionCodeServices), typeof(ActionCodeServices)},
                { typeof(ICodeMapperCommand), typeof(CodeMapperRepository)},
                { typeof(ICodeMapperQuery), typeof(CodeMapperRepository)},
                { typeof(ICodeMapperDetailCommand), typeof(CodeMapperDetailRepository)},
                { typeof(ICodeMapperDetailQuery), typeof(CodeMapperDetailRepository)},
                { typeof(ICodeMapperServices), typeof(CodeMapperServices)},
                { typeof(IReadCodeMapperServices), typeof(CodeMapperServices)},
                { typeof(ICodeMapperDetailServices), typeof(CodeMapperDetailServices)},
                { typeof(IPostingCommand), typeof(PostingRepository)},
                { typeof(IPostingQuery), typeof(PostingRepository)},
                { typeof(IPostingDetailCommand), typeof(PostingDetailRepository)},
                { typeof(IPostingDetailQuery), typeof(PostingDetailRepository)},
                { typeof(IPostingServices), typeof(PostingServices)},
                { typeof(IPostingDetailServices), typeof(PostingDetailServices)}
            };
        }
    }
}
