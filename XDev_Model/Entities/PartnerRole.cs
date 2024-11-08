namespace XDev_Model.Entities
{
    public class PartnerRole : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<PartnerRoles> Roles { get; set; }
    }
}
