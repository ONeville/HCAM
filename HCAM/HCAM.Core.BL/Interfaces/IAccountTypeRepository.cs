using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IAccountTypeCommand
    {
        Result<int> Add(AccountTypeItem entity);
        Result<int> Delete(int id);
        Result<AccountTypeItem> Update(int id, object entity);
    }
    public interface IAccountTypeQuery
    {
        Result<IEnumerable<AccountTypeItem>> Find(object criteria = null);
        Task<Result<IEnumerable<AccountTypeItem>>> FindAsync(object criteria = null);
        Result<AccountTypeItem> GetById(int id);
    }
}