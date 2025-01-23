namespace XDev_Model.Entities
{
    public class PriceSchemeCondition:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid PriceSchemeId { get; set; }
        public PriceScheme PriceScheme { get; set; }
        public Guid PriceConditionId { get; set; }
        public PriceCondition PriceCondition { get; set; }
        public short Position { get; set; }
        
    }
}
