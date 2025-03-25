namespace XDev_UnitWork.DTO.Invoice
{
    public class InvoiceListDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceType {  get; set; }
        public string CompanyCode {  get; set; }
        public string BranchCode {  get; set; }
        public string PartnerCode { get; set; }
        public bool Sporadic {  get; set; }
        public string CurrencyCode { get; set; }
        public decimal Total {  get; set; }
        public bool Canceled { get; set; }
        public bool Contingency { get; set; }
        public string Status {  get; set; }
        public DateTime CreatedAt {  get; set; }
    }
}
