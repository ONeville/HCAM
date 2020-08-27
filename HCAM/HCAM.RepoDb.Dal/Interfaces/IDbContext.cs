using System.Data;
using HCAM.Common.Extensions;
using HCAM.Common.Wrappers;

namespace HCAM.RepoDb.Dal.Interfaces
{
    public interface IDbContext
    {
        Result<IDbConnection> DbConnection { get; }
    }
}
