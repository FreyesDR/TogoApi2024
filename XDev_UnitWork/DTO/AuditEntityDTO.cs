namespace XDev_UnitWork.DTO
{
    public class AuditEntityDTO
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }

        public string LastUpdatedBy { get; set; } = string.Empty;
        public DateTime LastUpdatedAt { get; set; }

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }
}
