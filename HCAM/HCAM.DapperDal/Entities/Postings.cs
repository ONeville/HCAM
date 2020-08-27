using System;
using HCAM.DapperDal.Mapper;

namespace HCAM.DapperDal.Entities
{
    public class Postings
    {
        [Key]
        public int Id { get; set; }
        public int CodeMappingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool Post { get; set; }
    }
}