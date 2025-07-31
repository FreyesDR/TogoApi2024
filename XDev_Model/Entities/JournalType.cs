namespace XDev_Model.Entities
{
    public class JournalType:AuditEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public HashSet<Journal> Journals { get; set; }
    }
}
