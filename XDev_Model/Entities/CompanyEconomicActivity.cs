namespace XDev_Model.Entities
{
    public class CompanyEconomicActivity : AuditEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid EconomicActivityId { get; set; }
        public EconomicActivity EconomicActivity { get; set; }
        public bool Principal { get; set; }
    }
}
