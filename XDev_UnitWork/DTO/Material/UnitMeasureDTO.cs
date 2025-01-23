namespace XDev_UnitWork.DTO.Material
{
    public class UnitMeasureDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string AltCode { get; set; }
        public string Name { get; set; }
    }
}
