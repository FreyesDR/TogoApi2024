namespace XDev_UnitWork.DTO
{
    public class AddressPhoneDTO
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public string Phone { get; set; }
        public string PhoneExt { get; set; }
    }
}