using System;
using System.Data;
using System.Data.SqlClient;
using HCAM.DapperDal.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HCAM.DapperDal
{
    public class DbContext : IDbContext
    {
        internal const string CONNECTIONSTRING_KEY = "ConnectionString";
        private IDbConnection _connection;
        private string _connectionString;
        public IDbConnection Connection
        {
            get => string.IsNullOrEmpty(_connection.ConnectionString) ? InitConnection() : _connection;
            set => _connection = value;
        }
        public DbContext (IConfiguration configuration)
        {
            _connectionString = configuration.GetSection(CONNECTIONSTRING_KEY).Value;
            _connection = InitConnection();
        }

        private IDbConnection InitConnection ()
        {
            if (string.IsNullOrWhiteSpace(_connectionString))
                throw new ArgumentNullException("Connection string not found");
            return new SqlConnection(_connectionString);
        }
    }
}
