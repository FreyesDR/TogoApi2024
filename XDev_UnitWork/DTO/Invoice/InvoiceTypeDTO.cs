namespace XDev_UnitWork.DTO.Invoice
{
    public class InvoiceTypeDTO : AuditEntityDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid RangeId { get; set; }
    }
}
