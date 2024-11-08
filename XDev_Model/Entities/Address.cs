using System.Diagnostics.Metrics;

namespace XDev_Model.Entities
{
    public class Address:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid AddressTypeId { get; set; }
        public AddressType AddressType { get; set; }
        public string Address1 { get; set; }
        public Guid? CountryId { get; set; }
        public Country Country { get; set; }
        public Guid? RegionId { get; set; }
        public Region Region { get; set; }
        public Guid? CityId { get; set; }
        public City City { get; set; }
        public HashSet<AddressEmail> Emails { get; set; }
        public HashSet<AddressPhone> Phones { get; set; }
        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid? BranchId { get; set; }
        public Branch Branch { get; set; }
        public Guid? PartnerId { get; set; }
        public Partner Partner { get; set; }
    }
}
