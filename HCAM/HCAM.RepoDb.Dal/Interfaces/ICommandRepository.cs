using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using RepoDb;

namespace HCAM.RepoDb.Dal.Interfaces
{
    public interface ICommandRepository<T> where T : DataEntity
    {
        Result<object> Add(T entity);
        Result<int> AddBulk(IEnumerable<T> entities);
        Result<Task<int>> AddBulkAsync(IEnumerable<T> entities);
        Result<int> Delete(int id);
        Result<T> Update(object entity, int id);
        Result<int> InlineUpdate (object entity, int id);
        Result<int> ExecuteSqlNonQuery (string sql, object param);
    }
}