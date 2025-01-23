namespace XDev_UnitWork.DTO.Company
{
    public class CompanyListDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CompanyType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public bool Active { get; set; }
    }
}
