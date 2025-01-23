namespace XDev_Model.Entities
{
    public class MaterialFeatures:AuditEntity
    {
        public Guid Id { get; set; }
        public short NumType { get; set; }
        public Guid RangeId { get; set; }
    }
}
