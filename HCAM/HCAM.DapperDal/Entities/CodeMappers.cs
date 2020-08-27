using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class CodeMappers
    {
        [Key]
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string MapperName { get; set; }
    }
}