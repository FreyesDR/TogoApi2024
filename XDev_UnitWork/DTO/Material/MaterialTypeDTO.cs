namespace XDev_UnitWork.DTO.Material
{
    public class MaterialTypeDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
