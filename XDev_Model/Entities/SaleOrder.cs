namespace XDev_Model.Entities
{
    public class SaleOrder:AuditEntity
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date {  get; set; }
        public Guid SaleOrderTypeId { get; set; }
        public SaleOrderType SaleOrderType { get; set; }
        public Guid? PartnerId { get; set; }
        public Guid PaymentConditionId { get; set; }
        public PaymentCondition PaymentCondition { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }   
        public decimal DiscountPorcent {  get; set; }
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PointSaleId { get; set; }
        public string PointSaleCode {  get; set; }
        public string RefDocument {  get; set; }
        public DateTime RefDate { get; set; }
        public bool Delivered { get; set; }
        public bool Invoiced { get; set; }
        public bool Sporadic {  get; set; }
        public decimal Per1 {  get; set; }
        public decimal Ret1 {  get; set; }
        public decimal Ret10 { get; set; }
        public string Assignment {  get; set; }
        public Guid AssignmentId { get; set; }
        public Guid RecintoFiscalId { get; set; }
        public string RecintoFiscalCode {  get; set; }
        public Guid RegimenExportId {  get; set; }
        public string RegimenExportCode { get; set; }
        public Guid IncoTermsId { get; set; }
        public string IncoTerms { get; set; }
        public SaleOrderSporadicPartner SaleOrderSporadicPartner { get; set; }
        public HashSet<SaleOrderPosition> Positions { get; set; }
        public HashSet<SaleOrderPayment> Payments { get; set; }
    }
}
