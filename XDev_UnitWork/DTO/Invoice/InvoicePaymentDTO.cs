namespace XDev_UnitWork.DTO.Invoice
{
    public class InvoicePaymentDTO
    {        
        public string MeanOfPaymentCode { get; set; }
        public string MeanOfPayment { get; set; }
        public int Position { get; set; }
        public decimal Amount { get; set; }
        public string Reference { get; set; }
        public string Tipo { get; set; } // Contado o crédito
        public string Plazo { get; set; }
        public int Periodo { get; set; }
    }
}