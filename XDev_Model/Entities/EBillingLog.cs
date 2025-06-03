namespace XDev_Model.Entities
{
    public class EBillingLog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PointSaleId { get; set; }
        public Guid CodGen { get; set; }
        public string NumControl { get; set; }
        public string SelloRecibido { get; set; }
        public string TipoDte { get; set; }        
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; } = DateTime.Now;
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string StatusCode { get; set; }
        public string Observaciones {  get; set; }
        public Guid InvoiceId { get; set; }
        public Guid SaleOrderId {  get; set; }
        public bool Cancel {  get; set; }
        public Guid CancelInvoiceId { get; set; }
        public bool IsProd {  get; set; }
    }
}
