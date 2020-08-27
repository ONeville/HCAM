using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IEntryCommand
    {
        Result<int> Add(EntryItem entity);
        Result<int> Delete(int id);
        Result<EntryItem> Update(int id, object entity);
    }
    public interface IEntryQuery
    {
        Result<IEnumerable<EntryItem>> Find(object criteria = null);
        Task<Result<IEnumerable<EntryItem>>> FindAsync (object criteria = null);
        Result<EntryItem> GetById(int id);
    }
}