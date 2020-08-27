using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class JournalEntries
    {
        [Key]
        public int Id { get; set; }
        public int DebitAccountId { get; set; }
        public int CreditAccountId { get; set; }
        public int UserId { get; set; }
        public int EntryId { get; set; }
        public string Memo { get; set; }
    }
}