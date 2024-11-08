namespace XDev_UnitWork.DTO
{
    public class CountryDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string CodeMH { get; set; }
        public string Name { get; set; }
    }
}
