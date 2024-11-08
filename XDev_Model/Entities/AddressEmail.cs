namespace XDev_Model.Entities
{
    public class AddressEmail
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public bool Principal { get; set; }
    }
}
