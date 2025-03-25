namespace XDev_UnitWork.DTO
{
    public class UserInfoDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {get; set;}
        public string IDType { get; set;}
        public string IDNumber { get; set;}
        public IList<string> Roles { get; set; }
    }
}
