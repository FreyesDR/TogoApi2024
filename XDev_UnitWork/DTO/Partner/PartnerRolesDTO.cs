namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerRolesDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public Guid PartnerId { get; set; }
    }
}
