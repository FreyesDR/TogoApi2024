namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerFeaturesDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public short NumType { get; set; }
        public Guid RangeId { get; set; }
    }
}
