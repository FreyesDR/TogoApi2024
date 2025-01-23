using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public Guid SaleOrderTypeId { get; set; }
        public Guid? PartnerId { get; set; }
        public Guid CurrencyId { get; set; }
        public string PartnerCode { get; set; } = string.Empty;
        public string PartnerName { get; set; } = string.Empty ;
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }        
        public Guid CompanyId { get; set; }
        public Guid BranchId { get; set; }
        public Guid PointSaleId {  get; set; }
        public string RefDocument { get; set; } = string.Empty;
        public DateTime RefDate { get; set; }
        public bool Delivered { get; set; }
        public bool Invoiced { get; set; }
        public bool Sporadic {  get; set; }
        public decimal Per1 { get; set; }
        public decimal Ret1 { get; set; }
        public decimal Ret10 { get; set; }

        public bool HasPer1 => Per1 == 0 ? false : true;
        public bool HasRet1 => Ret1 == 0 ? false : true;
        public bool HasRet10 => Ret10 == 0 ? false : true;
        public SaleOrderSporadicPartnerDTO SaleOrderSporadicPartner { get; set; } = new SaleOrderSporadicPartnerDTO();
        public List<SaleOrderPositionDTO> Positions { get; set; } = new List<SaleOrderPositionDTO>();
    }
}
