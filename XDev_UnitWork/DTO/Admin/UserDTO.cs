namespace XDev_UnitWork.DTO.Admin
{
    public class UserDTO
    {    
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string Email { get; set; }
        public string Name { get; set; }
        public Guid IDTypeId { get; set; }
        public string IDNumber { get; set; }
        public string IDCode { get; set; }
        public string PhoneNumber {  get; set; }
        public bool Active { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    public class UserCreateDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public Guid IDTypeId { get; set; }
        public string IDNumber { get; set; }
        public string IDCode { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public string ConcurrencyStamp { get; set; }
    }

    public class UserListDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string IDNumber { get; set; }
        public bool Active { get; set; }
    }
}
