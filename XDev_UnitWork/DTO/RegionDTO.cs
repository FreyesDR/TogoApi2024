namespace XDev_UnitWork.DTO
{
    public class RegionDTO: AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CountryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
