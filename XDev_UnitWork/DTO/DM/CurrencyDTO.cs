namespace XDev_UnitWork.DTO.DM
{
    public class CurrencyDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string ISOCode { get; set; }
        public string Name { get; set; }
    }
}
