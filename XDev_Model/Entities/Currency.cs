namespace XDev_Model.Entities
{
    public class Currency : AuditEntity 
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string ISOCode { get; set; }
        public string Name { get; set; }
    }
}
