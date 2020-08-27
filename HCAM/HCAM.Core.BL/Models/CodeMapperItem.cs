using System.Collections.Generic;

namespace HCAM.Core.BL.Models
{
    public class CodeMapperItem
    {
        public int Id { get; set; }
        public bool IsDefault { get; set; }
        public string MapperName { get; set; }
        public IEnumerable<CodeMapperDetailItem> DetailItems { get; set; }
        public ItemState EntityState { get; set; }
    }
}