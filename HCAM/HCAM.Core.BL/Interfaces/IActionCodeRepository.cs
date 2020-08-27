using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IActionCodeCommand
    {
        Result<int> Add(ActionCodeItem entity);
        Result<int> Delete(int id);
        Result<ActionCodeItem> Update(int id, object entity);
        Result<long> GetLastCode(int code);
    }
    public interface IActionCodeQuery
    {
        Result<IEnumerable<ActionCodeItem>> Find(object criteria = null);
        Task<Result<IEnumerable<ActionCodeItem>>> FindAsync(object criteria = null);
        Result<ActionCodeItem> GetById(int id);
    }
}