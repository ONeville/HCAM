using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.Core.BL.Models;

namespace HCAM.Core.BL.Interfaces
{
    public interface IUserCommand
    {
        Result<int> Add(UserItem entity);
        Result<int> Delete(int id);
        Result<UserItem> Update(int id, object entity);
    }
    public interface IUserQuery
    {
        Result<IEnumerable<UserItem>> Find(object criteria = null);
        Task<Result<IEnumerable<UserItem>>> FindAsync (object criteria = null);
        Result<UserItem> GetById(int id);
    }
}