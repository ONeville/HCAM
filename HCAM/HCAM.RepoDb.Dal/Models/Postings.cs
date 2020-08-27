using System;
using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class Postings : DataEntity
    {
        public int Id { get; set; }
        public int CodeMappingId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool Post { get; set; }
    }
}