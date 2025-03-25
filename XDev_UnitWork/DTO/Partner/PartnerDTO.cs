namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PartnerTypeId { get; set; }
        public string Code { get; set; }
        public string OldCode { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public bool Active { get; set; }
        public string NumType { get; set; }
        public Guid PaymentConditionId { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonIDNumber { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonEmail { get; set; }
    }
}
