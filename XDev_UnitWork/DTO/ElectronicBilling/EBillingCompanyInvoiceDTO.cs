using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingCompanyInvoiceDTO:AuditEntityDTO
    {
        public Guid EBillingId { get; set; }
        public Guid CompanyId { get; set; }        
        public Guid InvoiceTypeId { get; set; }        
        public Guid EBillingDocumentId { get; set; }        
        public long RangeStart { get; set; }
        public long RangeEnd { get; set; }
        public long Current { get; set; }
        public bool ReStartYear { get; set; }
        public int NextReStart { get; set; }
    }

    public class EBillingCompanyInvoiceListDTO
    {
        public Guid EBillingId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public Guid EBillingDocumentId { get; set; }
        public string EBillingDocumentName { get; set; }
        public long RangeStart { get; set; }
        public long RangeEnd { get; set; }
        public long Current { get; set; }
        public bool ReStartYear { get; set; }
        public int NextReStart { get; set; }
    }
}
