using System;

namespace HCAM.Core.BL.Filters
{
    public class EntryFilter
    {
        public int? Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime? EntryDate { get; set; }
        public string EntryDescription { get; set; }
    }
}