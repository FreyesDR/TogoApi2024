namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerCompanyDTO
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string TradeName { get; set; }
    }
}
