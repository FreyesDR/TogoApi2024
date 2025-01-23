using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingDocumentDTO:AuditEntityDTO
    {
        public Guid EBillingId { get; set; }        
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
