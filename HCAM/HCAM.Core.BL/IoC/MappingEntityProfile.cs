using HCAM.RepoDb.Dal.Models;

namespace HCAM.Core.BL.IoC
{
    public class MappingEntityProfile : AutoMapper.Profile
    {
        public MappingEntityProfile()
        {
            CreateMap<Models.UserItem, Users>().ReverseMap();
            CreateMap<Models.AccountTypeItem, AccountTypes>().ReverseMap();
            CreateMap<Models.AccountItem, Accounts>().ReverseMap();
            CreateMap<Models.JournalEntryItem, JournalEntries>().ReverseMap();
            CreateMap<Models.EntryItem, Entries>().ReverseMap();
            CreateMap<Models.ActionCodeItem, ActionCodes>().ReverseMap();
            CreateMap<Models.CodeMapperItem, CodeMappers>().ReverseMap();
            CreateMap<Models.CodeMapperDetailItem, CodeMapperDetails>().ReverseMap();
            CreateMap<Models.PostingItem, Postings>().ReverseMap();
        }
    }
}