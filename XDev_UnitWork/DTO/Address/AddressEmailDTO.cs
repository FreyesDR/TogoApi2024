namespace XDev_UnitWork.DTO.Address
{
    public class AddressEmailDTO
    {
        public Guid Id { get; set; }
        public Guid AddressId { get; set; }
        public string Email { get; set; }
        public bool Principal { get; set; }
    }
}