using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Invoice
{
    public class InvoiceDTO:AuditEntityDTO
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public string InvoiceType { get; set; }                
        public string PaymentCondition { get; set; }                
        public string Currency { get; set; }
        public string CurrencyCode { get; set; } 
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NoSujeto {  get; set; }
        public decimal NoGravado {  get; set; }
        public decimal Exento {  get; set; }
        public decimal Gravado {  get; set; }
        public decimal DiscountPorcent { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string PointSale { get; set; }
        public Guid? PartnerId { get; set; }
        public string PartnerCode {  get; set; }
        public string PartnerName {  get; set; }
        public string PartnerAddress { get; set; }
        public string PartnerEcoAct {  get; set; }
        public string PartnerNit {  get; set; }
        public string PartnerNrc {  get; set; }
        public string PartnerEmail {  get; set; }
        public string PartnerPhone {  get; set; }
        
        public string RefDocument { get; set; }
        public DateTime RefDate { get; set; }
        public bool Sporadic { get; set; }
        public decimal Per1 { get; set; }
        public decimal Ret1 { get; set; }
        public decimal Ret10 { get; set; }
        public string SaleOrder { get; set; }
        public bool Canceled { get; set; }
        public DateTime CanceledDate { get; set; }
        public string CanceledUserId { get; set; }
        public string NumControl { get; set; }
        public string CodGeneracion { get; set; }
        public string SelloRecepcion { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string EBillingDoc { get; set; }
        public string Assignment { get; set; }
        public bool HasAssignment { get; set; }
        public string RecintoFiscal { get; set; }                
        public string RegimenExport { get; set; }
        public string IncoTerms { get; set; }    
        public bool Export {  get; set; }
        public bool Contingency {  get; set; }
        public InvoiceSporadicPartnerDTO InvoiceSporadicPartner { get; set; }
        public List<InvoicePositionDTO> Positions { get; set; }
        public List<InvoicePaymentDTO> Payments { get; set; }
    }
}
