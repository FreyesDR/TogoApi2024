namespace XDev_UnitWork.DTO.Company
{
    public class BranchTypeDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
