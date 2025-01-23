namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerRoleDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
