namespace XDev_UnitWork.DTO.Company
{
    public class CompanyEconomicActivityDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CompanyId { get; set; }
        public Guid EconomicActivityId { get; set; }
        public string EconomicActivityCode { get; set; }
        public string EconomicActivityName { get; set; }
        public bool Principal { get; set; }
    }
}
