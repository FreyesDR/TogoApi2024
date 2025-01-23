namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerEconomicActivityDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PartnerId { get; set; }
        public Guid EconomicActivityId { get; set; }
        public string EconomicActivityCode { get; set; }
        public string EconomicActivityName { get; set; }
        public bool Principal { get; set; }
    }
}
