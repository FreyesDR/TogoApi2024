namespace XDev_Model.Entities
{
    public class PartnerCompany
    {
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
