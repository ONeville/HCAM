namespace HCAM.Core.BL.Filters
{
    public class JournalEntryFilter
    {
        public int? Id { get; set; }
        public int? DebitAccountId { get; set; }
        public string DebitAccount { get; set; }
        public int? CreditAccountId { get; set; }
        public string CreditAccount { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? EntryId { get; set; }
        public string EntryDescription { get; set; }
        public string Memo { get; set; }
    }
}