using System;
using System.Data;
using System.Data.SqlClient;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;
using static HCAM.Common.Extensions.CommonExtensions;
using HCAM.RepoDb.Dal.Interfaces;
using RepoDb.Extensions;

namespace HCAM.RepoDb.Dal.Extensions
{
    public static class DalExtensions
    {
        public static Result<T> Command<T>(this IDbContext context, Func<IDbConnection, T> f) 
            => ExecuteRequest(context, f);

        public static Result<T> ExecuteRequest<T>(this IDbContext context, Func<IDbConnection, T> f) 
            => context.DbConnection.Bind(conn => GetDbConnection(conn, f));

        public static Result<T> GetDbConnection<T> (this IDbConnection conn, Func<IDbConnection, T> f) 
            => conn.TryCatch(str => conn.Using(f).ToResult()).Unbind();

        private static Result<T> SaveTransactionWithRollback<T>(Func<IDbConnection, IDbTransaction, T> f, IDbConnection db)
        {
            Result<T> result;
            var transaction = db.BeginTransaction();
            try
            {
                result = Result<T>.Ok(f(db, transaction));
                transaction.Commit();
            }
            catch (Exception e)
            {
                transaction.Rollback();
                result = Result<T>.Fail<T>(e.Message);
            }
            finally
            {
                transaction.Dispose();
            }
            return result;
        }
    }
}
