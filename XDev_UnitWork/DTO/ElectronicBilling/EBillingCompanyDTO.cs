using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingCompanyDTO:AuditEntityDTO
    {
        public Guid EBillingId { get; set; }        
        public Guid CompanyId { get; set; }
        public bool IsProd { get; set; }
        public Guid AddressId { get; set; }
        public Guid Nif1Id { get; set; }
        public Guid Nif2Id { get; set; }
        public bool Active { get; set; }
    }

    public class EBillingCompanyListDTO
    {
        public Guid EBillingId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string Nif1 { get; set; }
        public string Nif2 { get; set; }
        public bool IsProd { get; set; }
        public bool Active { get; set; }
    }

    public class EBillingCompanyAddressDTO
    {
        public Guid AddressId { get; set; }
        public string AddressName { get; set; }
    }

    public class EBillingCompanyIDsDTO
    {
        public Guid DocumentId { get; set; }
        public string Document { get; set; }
    }
}
