using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingTaxDTO:AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid EBillingId { get; set; }        

        public string TaxCode { get; set; }
        public string TaxName { get; set; }
    }
}
