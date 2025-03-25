namespace XDev_Model.Entities
{
    public class InvoicePosition
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public int Position { get; set; }
        public Guid MaterialId { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public decimal GrossPrice { get; set; }
        public decimal NetPrice { get; set; }
        public string PriceType { get; set; } // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado
        public Guid UnitMeasureId { get; set; }
        public string UnitMeasureCode { get; set; }
        public string UnitMeasureAltCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public Guid WareHouseId { get; set; }
        public HashSet<InvoicePositionCondition> Conditions { get; set; }
    }
}