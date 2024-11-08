namespace XDev_Model.Entities
{
    public class EconomicActivity : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<CompanyEconomicActivity> CompanyEconomicActivities { get; set; }
        public HashSet<PartnerEconomicActivity> PartnerEconomicActivities { get; set; }
    }
}
