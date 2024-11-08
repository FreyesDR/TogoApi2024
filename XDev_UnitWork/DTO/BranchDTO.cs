namespace XDev_UnitWork.DTO
{
    public class BranchDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BranchTypeId { get; set; }
        public Guid CompanyId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
