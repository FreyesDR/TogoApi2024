namespace XDev_Model.Entities
{
    public class SaleOrderPayment
    {        
        public Guid SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }        
        public Guid MeanOfPaymentId { get; set; }
        public string MeanOfPaymentCode { get; set; }
        public int Position { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Tipo { get; set; } // Contado o crédito
        public string Plazo { get; set; }
        public int Periodo {  get; set; }
    }
}
