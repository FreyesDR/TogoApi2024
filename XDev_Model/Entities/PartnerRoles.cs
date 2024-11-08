namespace XDev_Model.Entities
{
    public class PartnerRoles
    {
        public Guid RoleId { get; set; }
        public PartnerRole Role { get; set; }
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
