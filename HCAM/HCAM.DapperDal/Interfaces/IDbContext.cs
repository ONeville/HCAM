using System.Data;

namespace HCAM.DapperDal.Interfaces
{
    public interface IDbContext
    {
        IDbConnection Connection { get; }
    }
}
