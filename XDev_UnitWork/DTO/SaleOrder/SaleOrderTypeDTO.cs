namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderTypeDTO : AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Delivery { get; set; }
        public bool Invoice { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public Guid RangeId { get; set; }
        public Guid PriceSchemeId { get; set; }
        public string PdfFormName { get; set; }
        public string Inventory { get; set; } // I - Incremento, R - Reducción, N - No Aplica
        public bool ApplyRet1 { get; set; }
        public bool ApplyRet10 { get; set; }
        public bool ApplyPer1 { get; set; }
    }
}
