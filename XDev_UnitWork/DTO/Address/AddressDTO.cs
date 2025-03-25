namespace XDev_UnitWork.DTO.Address
{
    public class AddressDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AddressTypeId { get; set; }
        public string AddressType { get; set; }
        public string AddressTypeCode { get; set; }
        public string Address1 { get; set; }
        public Guid? CountryId { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string CountryAltCode {  get; set; }
        public Guid? RegionId { get; set; }        
        public string Region { get; set; }
        public string RegionCode { get; set; }
        public Guid? CityId { get; set; }
        public string City { get; set; }
        public string CityCode { get; set; }
        public List<AddressEmailDTO> Emails { get; set; }
        public List<AddressPhoneDTO> Phones { get; set; }
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PartnerId { get; set; }
    }
}
