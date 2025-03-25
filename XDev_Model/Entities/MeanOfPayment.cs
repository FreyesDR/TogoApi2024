namespace XDev_Model.Entities
{
    public class MeanOfPayment:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }        
    }
}
