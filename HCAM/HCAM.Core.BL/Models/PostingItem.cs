using System;
using System.Collections.Generic;

namespace HCAM.Core.BL.Models
{
    public class PostingItem
    {
        public int Id { get; set; }
        public int CodeMappingId { get; set; }
        public string CodeMapping { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateText => StartDate.ToShortDateString();
        public DateTime EndDate { get; set; }
        public string EndDateText => EndDate.ToShortDateString();
        public string Description { get; set; }
        public bool Post { get; set; }
        public ItemState EntityState { get; set; }
        public IEnumerable<PostingDetailItem> DetailItems { get; set; }
    }
}