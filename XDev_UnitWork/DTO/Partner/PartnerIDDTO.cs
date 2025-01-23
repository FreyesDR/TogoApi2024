namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerIDDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid PartnerId { get; set; }
        public Guid IDTypeId { get; set; }
        public string IDType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DateIssue { get; set; }
        public DateTime? DateExpira { get; set; }
        public Guid CountryId { get; set; }
        public Guid RegionId { get; set; }
        public string NIFNum {  get; set; }
    }
}
