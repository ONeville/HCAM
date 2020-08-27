using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class CodeMapperDetails : DataEntity
    {
        public int Id { get; set; }
        public string AccountingCode { get; set; }
        public int CodeMapperId { get; set; }
        public int ActionCodeId { get; set; }
        public int CreditAccountId { get; set; }
        public int DebitAccountId { get; set; }

    }
}