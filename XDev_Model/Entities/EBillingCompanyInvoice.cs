namespace XDev_Model.Entities
{
    public class EBillingCompanyInvoice:AuditEntity
    {
        public Guid EBillingId { get; set; }        
        public Guid CompanyId { get; set; }
        public EBillingCompany EBillingCompany { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public Guid EBillingDocumentId { get; set; }
        public long RangeStart { get; set; }
        public long RangeEnd { get; set; }
        public long Current { get; set; }
        public bool ReStartYear {  get; set; }
        public int NextReStart {  get; set; }
    }
}
