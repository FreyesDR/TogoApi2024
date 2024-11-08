namespace XDev_Model.Entities
{
    public class Country:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CodeMH { get; set; }
        public string Name { get; set; }
        public HashSet<Region> Regions { get; set; }
        public HashSet<Address> Addresses { get; set; }
    }
}
