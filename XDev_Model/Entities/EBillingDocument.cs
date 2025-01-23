namespace XDev_Model.Entities
{
    public class EBillingDocument:AuditEntity
    {
        public Guid EBillingId { get; set; }
        public EBilling EBilling { get; set; }
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
