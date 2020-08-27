using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class AccountTypes
    {
        [Key]
        public int Id { get; set; }
        public string AccountTypeName { get; set; }
    }
}