namespace XDev_UnitWork.DTO.Address
{
    public class CountryDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string CodeMH { get; set; }
        public string Name { get; set; }
    }
}
