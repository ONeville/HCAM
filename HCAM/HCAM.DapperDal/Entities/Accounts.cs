using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class Accounts
    {
        [Key]
        public int Id { get; set; }
        public string AccountName { get; set; }
        public int AccountTypeId { get; set; }
    }
}