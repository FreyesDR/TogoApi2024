namespace XDev_UnitWork.DTO.PriceScheme
{
    public class PriceSchemeConditionDTO:AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid PriceSchemeId { get; set; }        
        public Guid PriceConditionId { get; set; }
        public PriceConditionDTO PriceCondition { get; set; }
        public short Position { get; set; }
    }
}
