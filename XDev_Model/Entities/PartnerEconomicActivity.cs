namespace XDev_Model.Entities
{
    public class PartnerEconomicActivity : AuditEntity
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public Guid EconomicActivityId { get; set; }
        public EconomicActivity EconomicActivity { get; set; }
        public bool Principal { get; set; }
    }
}
