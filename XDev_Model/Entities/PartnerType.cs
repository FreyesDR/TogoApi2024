namespace XDev_Model.Entities
{
    public class PartnerType : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Partner> Partners { get; set; }
    }
}
