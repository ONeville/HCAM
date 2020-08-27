using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class JournalEntries : DataEntity
    {
        public int Id { get; set; }
        public int DebitAccountId { get; set; }
        public int CreditAccountId { get; set; }
        public int UserId { get; set; }
        public int EntryId { get; set; }
        public string Memo { get; set; }
    }
}