namespace XDev_UnitWork.DTO.Address
{
    public class CityDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid RegionId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
