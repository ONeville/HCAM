using System.Collections.Generic;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IJournalEntryCommand
    {
        Result<int> Add(JournalEntryItem entity);
        Result<int> Delete(int id);
        Result<JournalEntryItem> Update(int id, object entity);
    }
    public interface IJournalEntryQuery
    {
        Result<IEnumerable<JournalEntryItem>> Find(JournalEntryFilter criteria = null);
        Result<JournalEntryItem> GetById(int id);
    }
}