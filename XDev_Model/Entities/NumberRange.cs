namespace XDev_Model.Entities
{
    public class NumberRange:AuditEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public long NumStart { get; set; }
        public long NumEnd { get; set; }
        public long NumCurrent { get; set; }
        public bool Active { get; set; }        
    }
}
