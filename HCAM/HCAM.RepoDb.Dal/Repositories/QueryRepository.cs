using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using HCAM.RepoDb.Dal.Enums;
using HCAM.RepoDb.Dal.Extensions;
using HCAM.RepoDb.Dal.Interfaces;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Extensions;
using RepoDb.Reflection;
using static HCAM.RepoDb.Dal.Extensions.DalExtensions;
using static HCAM.Common.Extensions.CommonExtensions;

namespace HCAM.RepoDb.Dal.Repositories
{
    public class QueryRepository<T> : IQueryRepository<T> where T : DataEntity
    {
        private readonly IDbContext _context;
        public QueryRepository(IDbContext context)
        {
            _context = context;
        }

        public Result<IEnumerable<T>> Get (object filter = null) 
            => filter == null 
            ? _context.Command(db => db.Query<T>()) 
            : _context.Command(db => db.Query<T>(filter));
        public Result<T> GetById(int id) 
            => _context.Command(db => db.Query<T>(db, id).FirstOrDefault()).ToResult();

        public Result<Task<IEnumerable<T>>> ExecuteQueryAsync (string sql)
        {
            throw new NotImplementedException();
        }

        public Result<Task<IEnumerable<T>>> GetAsync (object filter = null) 
            => filter == null 
            ? _context.Command(db => db.QueryAsync<T>())
            : _context.Command(db => db.QueryAsync<T>(filter));

        public Result<T> GetByIdAsync (int id) 
            => _context.Command(db => db.QueryAsync<T>(db, id).Result.FirstOrDefault()).ToResult();

        public Result<IEnumerable<T>> ExecuteSqlQuery (string sql) 
            => _context.Command(db =>
                db.CreateCommand(sql, CommandType.Text, 500, null)
                    .ToResult()
                    .Bind(cmd => cmd.ExecuteReader().Using(reader => DataReaderConverter.ToEnumerable<T>((DbDataReader)reader)).ToResult()))
            .Unbind();

        public Result<IEnumerable<T>> ExecuteSqlQuery (string sql, object filter) 
            => _context.Command(db => db.ExecuteQuery<T>(sql, filter));
        public Result<Task<IEnumerable<T>>> ExecuteSqlQueryAsync(string sql, object filter = null) 
            => _context.Command(db => db.ExecuteQueryAsync<T>(sql, filter));

        public Result<long> CountRecord (dynamic filter)
        {
            var query = BuildSingleQueryFilter(filter, (OperationFilter)filter.Operation) as QueryField;
            return _context.Command(db => db.Count<T>(query));
        }

        private object BuildQueryFilter (dynamic filter, OperationFilter operation, object field)
        {
            return BuildSingleQueryFilter(filter, operation);
        }

        public QueryField BuildSingleQueryFilter(dynamic filter, OperationFilter operation)
        {
            switch (operation)
            {
                case OperationFilter.Equal:
                    return new QueryField(filter.Field, Operation.Equal, filter.Value);
                case OperationFilter.NotEqual:
                    return new QueryField(filter.Field, Operation.NotEqual, filter.Value);
                case OperationFilter.LessThan:
                    return new QueryField(filter.Field, Operation.LessThan, filter.Value);
                case OperationFilter.GreaterThan:
                    return new QueryField(filter.Field, Operation.GreaterThan, filter.Value);
                case OperationFilter.LessThanOrEqual:
                    return new QueryField(filter.Field, Operation.LessThanOrEqual, filter.Value);
                case OperationFilter.GreaterThanOrEqual:
                    return new QueryField(filter.Field, Operation.GreaterThanOrEqual, filter.Value);
                case OperationFilter.Like:
                    return new QueryField(filter.Field, Operation.Like, filter.Value);
                case OperationFilter.NotLike:
                    return new QueryField(filter.Field, Operation.NotLike, filter.Value);
                case OperationFilter.Between:
                    return new QueryField(filter.Field, Operation.Between, filter.Values);
                case OperationFilter.NotBetween:
                    return new QueryField(filter.Field, Operation.Equal, filter.Values);
                case OperationFilter.In:
                    return new QueryField(filter.Field, Operation.Equal, filter.Values );
                case OperationFilter.NotIn:
                    return new QueryField(filter.Field, Operation.Equal, filter.Values);
                case OperationFilter.All:
                    throw new NotImplementedException();
                case OperationFilter.Any:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }
        }
    }
}