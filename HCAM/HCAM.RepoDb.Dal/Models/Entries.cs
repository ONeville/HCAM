using System;
using RepoDb;

namespace HCAM.RepoDb.Dal.Models
{
    public class Entries : DataEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime EntryDate { get; set; }
        public string EntryDescription { get; set; }
    }
}