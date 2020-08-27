using System.Collections.Generic;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using RepoDb;

namespace HCAM.RepoDb.Dal.Interfaces
{
    public interface IQueryRepository<T> where T : DataEntity
    {
        Result<IEnumerable<T>> Get(object filter = null);
        Result<T> GetById(int id);
        Result<Task<IEnumerable<T>>> ExecuteQueryAsync(string sql);
        Result<Task<IEnumerable<T>>> GetAsync(object filter = null);
        Result<T> GetByIdAsync(int id);
        Result<IEnumerable<T>> ExecuteSqlQuery(string sql);
        Result<IEnumerable<T>> ExecuteSqlQuery(string sql, object filter);
        Result<Task<IEnumerable<T>>> ExecuteSqlQueryAsync (string sql, object filter = null);
        Result<long> CountRecord(dynamic filter);
    }
}