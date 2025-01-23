namespace XDev_Model.Entities
{
    public class PartnerID : AuditEntity
    {
        public Guid Id { get; set; }
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public Guid IDTypeId { get; set; }
        public IDType IDType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DateIssue { get; set; }
        public DateTime? DateExpira { get; set; }
        public Guid CountryId { get; set; }
        public Guid RegionId { get; set; }
        public string NIFNum {  get; set; }
    }
}
