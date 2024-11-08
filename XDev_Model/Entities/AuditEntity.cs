using XDev_Model.Interfaces;

namespace XDev_Model.Entities
{
    public class AuditEntity: IAuditEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
