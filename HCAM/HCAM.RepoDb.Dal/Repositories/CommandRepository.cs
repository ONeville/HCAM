using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.RepoDb.Dal.Interfaces;
using RepoDb;
using RepoDb.Extensions;
using RepoDb.Reflection;
using static HCAM.RepoDb.Dal.Extensions.DalExtensions;

namespace HCAM.RepoDb.Dal.Repositories
{
    public class CommandRepository<T> : ICommandRepository<T> where T : DataEntity
    {
        private readonly IDbContext _context;
        public CommandRepository (IDbContext context)
        {
            _context = context;
        }
        public Result<object> Add (T entity) 
            => _context.Command(db => db.Insert<T>(entity));

        public Result<T> Update (object entity, int id) 
            => FindId(id)
            .Bind(result => _context.Command(db => db.InlineUpdate<T>(entity, new { Id = id })))
            .Bind(idUpdated => FindId(idUpdated));

        public Result<int> InlineUpdate(object entity, int id) 
            => _context.Command(db => db.InlineUpdate<T>(entity, new { Id = id }));

        public Result<int> Delete(int id) => FindId(id).Bind(result 
            => _context.Command(db => db.Delete<T>(result)));

        public Result<int> AddBulk(IEnumerable<T> entities) 
            => _context.Command(db => db.BulkInsert(entities));

        public Result<Task<int>> AddBulkAsync(IEnumerable<T> entities) 
            => _context.Command(db => db.BulkInsertAsync(entities));

        public Result<int> ExecuteSqlNonQuery(string sql, object param) 
            => _context.Command(db => db.ExecuteNonQuery(sql, param));

        private Result<T> FindId(int id)
        {
            var item = _context.Command(db => db.Query<T>(id).FirstOrDefault());
            if (item.Failure || item.Value == null)
                return Result<T>.Fail<T>($"Cannot Delete, ID {id} not Found : {item.Error}");
            return item;
        }
    }
}