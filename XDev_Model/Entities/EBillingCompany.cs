namespace XDev_Model.Entities
{
    public class EBillingCompany: AuditEntity
    {
        public Guid EBillingId { get; set; }
        public EBilling EBilling { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsProd {  get; set; }
        public Guid AddressId { get; set; }           
        public Guid Nif1Id { get; set; }
        public Guid Nif2Id { get; set; }        
        public bool Active { get; set; }

        public HashSet<EBillingCompanyInvoice> Invoice { get; set; }
    }
}
