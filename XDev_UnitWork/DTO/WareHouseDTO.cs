namespace XDev_UnitWork.DTO
{
    public class WareHouseDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
