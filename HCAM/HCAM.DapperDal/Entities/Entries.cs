using System;
using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class Entries
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDescription { get; set; }
    }
}