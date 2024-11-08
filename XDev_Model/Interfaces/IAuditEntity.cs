namespace XDev_Model.Interfaces
{
    public interface IAuditEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
