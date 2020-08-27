using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using HCAM.DapperDal.Interfaces;
using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Repository
{
    public class Repository: IRepository
    {
        private readonly IDbContext _context;

        public Repository (IDbContext context)
        {
            _context = context;
        }

        public int Add<T> (T entity)
        {
            var ids = EntityMapper.MapProperties(entity).IdPairs;
            var sql = $@"INSERT INTO [{typeof(T).Name}] 
                        ({string.Join(", ", EntityMapper.MapProperties(entity).ValueNames)}) 
                         VALUES(@{string.Join(", @", EntityMapper.MapProperties(entity).ValueNames)}) ";
            if (ids.Any())
            {
                sql = sql + "SELECT CAST(scope_identity() AS int)";
            }
            try
            {
                using (var connection = _context.Connection)
                {
                    connection.Open();
                    var id = connection.Query<int>(sql, entity).SingleOrDefault();
                    if (ids.Any())
                    {
                        EntityMapper.SetId(entity, id, ids);
                    }
                    connection.Close();
                    return id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete<T>(int id)
        {
            var sql = $@"DELETE FROM [{typeof(T).Name}]  WHERE Id={id}";
            using (var connection = _context.Connection)
            {
                connection.Open();
                connection.Execute(sql, commandType: CommandType.Text);
                connection.Close();
            }
        }

        public int Update<T>(T entity)
        {
            var mapper = EntityMapper.MapProperties(entity);
            var sqlIdPairs = EntityMapper.GetSqlPairs(mapper.IdNames);
            var sqlValuePairs = EntityMapper.GetSqlPairs(mapper.ValueNames);
            var sql = $@"UPDATE [{typeof(T).Name}]  
                            SET {sqlValuePairs}
                        WHERE {sqlIdPairs}";
            try
            {
                using (var connection = _context.Connection)
                {
                    connection.Open();
                    var id = connection.Execute(sql, mapper.AllPairs, commandType: CommandType.Text);
                    connection.Close();
                    return id;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<T> Get<T>(T criteria = default(T))
        {
            string sql;
            if (criteria != null)
            {
                var mapper = EntityMapper.MapProperties(criteria);
                var sqlPairs = EntityMapper.GetSqlPairs(mapper.AllNames, " AND ");
                sql = $@"SELECT * FROM [{typeof(T).Name}] WHERE {sqlPairs}";
            }
            else
            {
                sql = $@"SELECT * FROM [{typeof(T).Name}]";
            }
            using (var connection = _context.Connection)
            {
                connection.Open();
                var response = connection.Query<T>(sql, commandType: CommandType.Text);
                connection.Close();
                return response;
            }
        }

        public async Task<IEnumerable<T>> GetAsync<T>()
        {
            using (var connection = _context.Connection)
            {
                connection.Open();
                return await connection.QueryAsync<T>($@"SELECT * FROM [{typeof(T).Name}]", commandType: CommandType.Text);
            }
        }
        
        public T GetById<T>(int id)
        {
            using (var connection = _context.Connection)
            {
                connection.Open();
                var response = connection
                    .Query<T>($@"SELECT * FROM [{typeof(T).Name}] WHERE Id={id}", commandType: CommandType.Text)
                    .SingleOrDefault();
                connection.Close();
                return response;
            }
        }
        
        public IEnumerable<object> ExecuteSql(string sql)
        {
            using (var connection = _context.Connection)
            {
                connection.Open();
                var response = connection.Query<object>(sql, commandType: CommandType.Text);
                connection.Close();
                return response;
            }
        }
        public int ExecuteSqlScalar(string sql)
        {
            using (var connection = _context.Connection)
            {
                connection.Open();
                var response = connection
                    .ExecuteScalar<int>(sql, commandType: CommandType.Text);
                connection.Close();
                return response;
            }
        }
    }
}
