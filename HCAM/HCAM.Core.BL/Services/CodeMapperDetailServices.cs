using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Interfaces;
using HCAM.Core.BL.Interfaces.Services;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Services
{
    public class CodeMapperDetailServices : ICodeMapperDetailServices
    {
        private readonly ICodeMapperDetailCommand _repository;
        private readonly ICodeMapperDetailQuery _query;

        public CodeMapperDetailServices(ICodeMapperDetailCommand repository, ICodeMapperDetailQuery query)
        {
            _repository = repository;
            _query = query;
        }
        
        public Result<IEnumerable<CodeMapperDetailItem>> GetDetails (int mapperId) => _query.GetDetails(mapperId);
        public Result<IEnumerable<CodeMapperDetailItem>> GenerateDetails (int mapperId) => _query.GenerateDetails(mapperId);
        public Result<int> Save (IEnumerable<CodeMapperDetailItem> entities)
        {
            if (!entities.Any()) return Result<int>.Fail<int>("No Data has been sent, the object is empty");

            var items = entities.Where(item => item.EntityState == ItemState.Unchanged);
            
            return GetDetails(entities.First().CodeMapperId)
                .Bind(result => _repository.Save(UpdateItems(result, items)).Result);
        }

        private IEnumerable<CodeMapperDetailItem> UpdateItems(IEnumerable<CodeMapperDetailItem> result, IEnumerable<CodeMapperDetailItem> items)
        {
            var itemToSave = items.Where(item => result.Any(old => _IsDirtyCode(old, item))).ToList();
            itemToSave.ForEach(x => x.EntityState = ItemState.Modified);
            return itemToSave;
        }
        private bool _IsDirtyCode(CodeMapperDetailItem y, CodeMapperDetailItem x) => y.ActionCode == x.ActionCode
                                                                                     && (y.AccountingCode != x.AccountingCode
                                                                                         || y.CreditAccountId != x.CreditAccountId
                                                                                         || y.DebitAccountId != x.DebitAccountId);
    }
}
