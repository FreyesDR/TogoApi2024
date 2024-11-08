namespace XDev_Model.Entities
{
    public class AddressPhone
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid AddressId { get; set; }
        
        public string Phone { get; set; }
        public string PhoneExt { get; set; }
    }
}
