namespace XDev_UnitWork.DTO
{
    public class CityDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
