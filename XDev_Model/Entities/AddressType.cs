using System.Net;

namespace XDev_Model.Entities
{
    public class AddressType: AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Address> Addresses { get; set; }
    }
}
