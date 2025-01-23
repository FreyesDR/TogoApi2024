namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderPositionDTO:AuditEntityDTO
    {
        public Guid Id { get; set; }
        public Guid SaleOrderId { get; set; }        
        public int Position { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string PriceType { get; set; } // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado
        public Guid UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountAmount { get; set; }        
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }        
        public Guid WareHouseId { get; set; }
        public List<SaleOrderPositionConditionDTO> Conditions { get; set; }
        public decimal Gravado => PriceType == "GV" ? NetAmount : 0;
        public decimal Exento => PriceType == "EX" ? NetAmount : 0;
        public decimal NoSujeto => PriceType == "NS" ? NetAmount : 0;
        public decimal NoGravado => PriceType == "NG" ? NetAmount : 0;
        public decimal UnitPrice => (NetAmount + Math.Abs(DiscountAmount)) / Quantity;
    }
}
