using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class Accounts : DataEntity
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int AccountTypeId { get; set; }
    }
}