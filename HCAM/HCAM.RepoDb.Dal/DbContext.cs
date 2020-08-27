using System;
using System.Data;
using System.Data.SqlClient;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using static HCAM.Common.Extensions.CommonExtensions;
using HCAM.RepoDb.Dal.Interfaces;
using Microsoft.Extensions.Configuration;
using RepoDb.Extensions;

namespace HCAM.RepoDb.Dal
{
    public class DbContext : IDbContext
    {
        internal const string CONNECTIONSTRING_KEY = "ConnectionString";
        private readonly Result<IDbConnection> _connection;
        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = InitConnection(configuration);
        }

        public Result<IDbConnection> DbConnection 
            => _connection.Success && string.IsNullOrEmpty(_connection.Value.ConnectionString)
            ? InitConnection(_configuration)
            : (_connection.Success
                ? _connection
                : Result<IDbConnection>.Fail<IDbConnection>("Db Connection failed to connect"));

        private Result<IDbConnection> GetConnection(string connStr) 
            => string.IsNullOrWhiteSpace(connStr) 
            ? Result<IDbConnection>.Fail<IDbConnection>("Connection string not found") 
            : Result<IDbConnection>.Ok(new SqlConnection(connStr).EnsureOpen());

        private Result<IDbConnection> InitConnection(IConfiguration configuration) 
            => CONNECTIONSTRING_KEY.TryCatch(str => configuration.GetSection(str).Value)
            .Bind(conn => GetConnection(conn));
    }
}
