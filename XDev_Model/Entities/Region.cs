namespace XDev_Model.Entities
{
    public class Region:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid CountryId { get; set; }
        public Country Country { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<City> Cities { get; set; }
        public HashSet<Address> Addresses { get; set; }
    }
}
