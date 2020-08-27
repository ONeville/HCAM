using System;

namespace HCAM.Core.BL.Models
{
    public class EntryItem
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDescription { get; set; }
    }
}