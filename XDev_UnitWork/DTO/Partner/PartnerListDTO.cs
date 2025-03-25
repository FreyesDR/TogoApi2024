namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerListDTO
    {
        public Guid Id { get; set; }
        public string PartnerType { get; set; }
        public string Code { get; set; }
        public string OldCode { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public Guid PaymentConditionId { get; set; }
        public bool Active { get; set; }
    }
}
