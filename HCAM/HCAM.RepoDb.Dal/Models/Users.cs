using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class Users : DataEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}