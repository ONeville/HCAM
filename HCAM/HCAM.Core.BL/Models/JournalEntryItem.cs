namespace HCAM.Core.BL.Models
{
    public class JournalEntryItem
    {
        public JournalEntryItem(int id, int debitAccountId, string debitAccount, int creditAccountId, string creditAccount, int userId, string processBy, int entryId, string entryDescription, string memo)
        {
            Id = id;
            DebitAccountId = debitAccountId;
            DebitAccount = debitAccount;
            CreditAccountId = creditAccountId;
            CreditAccount = creditAccount;
            UserId = userId;
            ProcessBy = processBy;
            EntryId = entryId;
            EntryDescription = entryDescription;
            Memo = memo;
        }

        public int Id { get; set; }
        public int DebitAccountId { get; set; }
        public string DebitAccount { get; set; }
        public int CreditAccountId { get; set; }
        public string CreditAccount { get; set; }
        public int UserId { get; set; }
        public string ProcessBy { get; set; }
        public int EntryId { get; set; }
        public string EntryDescription { get; set; }
        public string Memo { get; set; }
    }
}