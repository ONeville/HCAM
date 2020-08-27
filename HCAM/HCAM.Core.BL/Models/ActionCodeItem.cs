namespace HCAM.Core.BL.Models
{
    public class ActionCodeItem
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsFee { get; set; }
        public bool IsAsset { get; set; }
        public bool IsLiability { get; set; }
        public bool IsEquity { get; set; }
        public Frequency Frequency { get; set; }
        public string FrequencyName => Frequency.ToString();
    }
}