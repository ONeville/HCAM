using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}