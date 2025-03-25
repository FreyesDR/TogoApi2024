namespace XDev_Model.Entities
{
    public class EBillingTax:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid EBillingId { get; set; }
        public EBilling EBilling { get; set; }

        public string TaxCode {  get; set; }
        public string TaxName { get; set; }
    }
}
