namespace XDev_Model.Entities
{
    public class PriceScheme:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<PriceSchemeCondition> Conditions { get; set; }
    }
}
