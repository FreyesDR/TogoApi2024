namespace XDev_Model.Entities
{
    public class InvoiceType:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid RangeId { get; set; }           
    }
}
