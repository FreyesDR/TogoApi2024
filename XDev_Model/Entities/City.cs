namespace XDev_Model.Entities
{
    public class City:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public Region Region { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Address> Addresses { get; set; }
    }
}
