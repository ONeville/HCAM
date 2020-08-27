using System;
using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class PostingDetails
    {
        [Key]
        public int Id { get; set; }
        public int PostingId { get; set; }
        public int ActionCodeId { get; set; }
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string Memo { get; set; }
    }
}