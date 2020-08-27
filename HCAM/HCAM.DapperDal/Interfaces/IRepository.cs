using System.Collections.Generic;
using System.Threading.Tasks;

namespace HCAM.DapperDal.Interfaces
{
    public interface IRepository
    {
        // where T : class;
        int Add<T> (T entity);

        void Delete<T>(int id);

        int Update<T>(T entity);

        IEnumerable<T> Get<T>(T criteria);
        Task<IEnumerable<T>> GetAsync<T>();

        T GetById<T> (int id);

        IEnumerable<object> ExecuteSql(string sql);

        int ExecuteSqlScalar (string sql);
    }
}
