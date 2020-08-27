using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class AccountTypes : DataEntity
    {
        public int Id { get; set; }
        public string AccountTypeName { get; set; }
    }
}