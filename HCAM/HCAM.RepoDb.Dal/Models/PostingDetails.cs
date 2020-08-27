using System;
using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class PostingDetails : DataEntity
    {
        public int Id { get; set; }
        public int PostingId { get; set; }
        public int ActionCodeId { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Memo { get; set; }
    }
}