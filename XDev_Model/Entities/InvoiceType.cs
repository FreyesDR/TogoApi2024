namespace XDev_Model.Entities
{
    public class InvoiceType:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid RangeId { get; set; }
        public string FormName {  get; set; }
        public bool Export { get; set; }
    }
}
