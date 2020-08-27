using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class CodeMappers : DataEntity
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string MapperName { get; set; }
    }
}