using System;

namespace HCAM.Core.BL.Models
{
    public class PostingDetailItem
    {
        public int Id { get; set; }
        public int PostingId { get; set; }
        public string Posting { get; set; }
        public int ActionCodeId { get; set; }
        public int ActionCode { get; set; }
        public string ActionCodeName { get; set; }
        public string Description => ActionCode + " | " + ActionCodeName;
        public decimal BudgetAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string EffectiveDateText => EffectiveDate.ToShortDateString();
        public string Memo { get; set; }
        public string FrequencyName => Enum.GetName(typeof(Frequency), Frequency);
        public int Frequency { get; set; }
        public ItemState EntityState { get; set; }

        public bool MenuEffDt { get; set; } = false;
    }
}