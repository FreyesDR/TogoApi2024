namespace XDev_Model.Entities
{
    public class Invoice:AuditEntity
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid InvoiceTypeId { get; set; }
        public InvoiceType InvoiceType { get; set; }
        public Guid? PartnerId { get; set; }
        public Guid PaymentConditionId { get; set; }
        public PaymentCondition PaymentCondition { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal DiscountPorcent { get; set; }
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PointSaleId { get; set; }
        public string PointSaleCode { get; set; }
        public string RefDocument { get; set; }
        public DateTime RefDate { get; set; }        
        public bool Sporadic { get; set; }
        public decimal Per1 { get; set; }
        public decimal Ret1 { get; set; }
        public decimal Ret10 { get; set; }
        public Guid SaleOrderId { get; set; }
        public bool Canceled {  get; set; }
        public DateTime CanceledDate { get; set; }
        public string CanceledUserId {  get; set; }
        public string NumControl {  get; set; }
        public string CodGeneracion {  get; set; }
        public string SelloRecepcion {  get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string EBillingDoc {  get; set; }
        public string Assignment { get; set; }
        public Guid AssignmentId {  get; set; }
        public Guid RecintoFiscalId { get; set; }
        public string RecintoFiscalCode { get; set; }
        public Guid RegimenExportId { get; set; }
        public string RegimenExportCode { get; set; }
        public Guid IncoTermsId { get; set; }
        public string IncoTerms { get; set; }
        public bool SentEmail { get; set; }
        public bool Contingency {  get; set; }        
        public InvoiceSporadicPartner InvoiceSporadicPartner { get; set; }
        public HashSet<InvoicePosition> Positions { get; set; }
        public HashSet<InvoicePayment> Payments { get; set; }
    }
}
