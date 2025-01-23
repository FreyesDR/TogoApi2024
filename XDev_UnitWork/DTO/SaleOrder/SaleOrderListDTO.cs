namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderListDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string SaleOrderType { get; set; }
        public string PartnerCode { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public string CurrencyCode { get; set; }
        public string CompanyCode { get; set; }
        public string RefDocument { get; set; }
        public bool Delivered { get; set; }
        public bool Invoiced { get; set; }
    }
}
