namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingLogDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateTime { get; set; } = DateTime.Now;
        public Guid CompanyId { get; set; }
        public string CompanyCode {  get; set; }
        public Guid BranchId { get; set; }
        public string BranchCode { get; set; }
        public Guid PointSaleId { get; set; }
        public string PointSaleCode { get; set; }
        public Guid CodGen { get; set; }
        public string NumControl { get; set; }
        public string SelloRecibido { get; set; }
        public string TipoDte { get; set; }
        public string TipoDteName { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime ResponseDate { get; set; } = DateTime.Now;
        public string ResponseStatus { get; set; }
        public string ResponseStatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string StatusCode { get; set; }
        public string Observaciones { get; set; }
    }
}
