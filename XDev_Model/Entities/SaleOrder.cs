namespace XDev_Model.Entities
{
    public class SaleOrder:AuditEntity
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date {  get; set; }
        public Guid SaleOrderTypeId { get; set; }
        public Guid? PartnerId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }        
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PointSaleId { get; set; }
        public string RefDocument {  get; set; }
        public DateTime RefDate { get; set; }
        public bool Delivered { get; set; }
        public bool Invoiced { get; set; }
        public bool Sporadic {  get; set; }
        public decimal Per1 {  get; set; }
        public decimal Ret1 {  get; set; }
        public decimal Ret10 { get; set; }
        public SaleOrderSporadicPartner SaleOrderSporadicPartner { get; set; }
        public HashSet<SaleOrderPosition> Positions { get; set; }
    }
}
