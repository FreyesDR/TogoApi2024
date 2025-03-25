namespace XDev_Model.Entities
{
    public class InvoicePayment
    {
        public Guid InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
        public Guid MeanOfPaymentId { get; set; }
        public string MeanOfPaymentCode { get; set; }
        public int Position { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Tipo { get; set; } // Contado o crédito
        public string Plazo { get; set; }
        public int Periodo { get; set; }
    }
}
