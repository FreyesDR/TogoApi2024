namespace XDev_Model.Entities
{
    public class Journal: AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid JournalTypeId { get; set; }
        public JournalType JournalType { get; set; }
    }
}
