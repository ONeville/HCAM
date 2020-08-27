namespace HCAM.Core.BL.Models
{
    public class CodeMapperDetailItem
    {
        public int CodeMapperId { get; set; }
        public string CodeMapperName { get; set; }
        public int ActionCodeId { get; set; }
        public int ActionCode { get; set; }
        public string ActionCodeName { get; set; }
        public string Frequency { get; set; }
        public int CreditAccountId { get; set; }
        public string CreditAccountName { get; set; }
        public int DebitAccountId { get; set; }
        public string DebitAccountName { get; set; }
        public string AccountingCode { get; set; }
        public ItemState EntityState { get; set; }
    }
}