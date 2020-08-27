using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Filters;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IAccountCommand
    {
        Result<int> Add(AccountItem entity);
        Result<int> Delete(int id);
        Result<AccountItem> Update(int id, object entity);
    }
    public interface IAccountQuery
    {
        Result<IEnumerable<AccountItem>> Find(object criteria = null);
        Task<Result<IEnumerable<AccountItem>>> FindAsync(object criteria = null);
        Result<AccountItem> GetById(int id);
        Result<IEnumerable<AccountItem>> GetDetails (AccountFilter criteria);
    }
}