namespace XDev_Model.Entities
{
    public class RegimenExport:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
