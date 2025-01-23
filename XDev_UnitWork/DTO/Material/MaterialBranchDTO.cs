using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Material
{
    public class MaterialBranchDTO:AuditEntityDTO
    {        
        public Guid MaterialId { get; set; }        
        public Guid BranchId { get; set; }         

        public decimal PriceSale { get; set; }
        public bool IsLockedSale { get; set; }

        public decimal PricePurchase { get; set; }
        public bool IsLockedPurchase { get; set; }
        
    }

    public class MaterialBranchListDTO
    {
        public Guid MaterialId { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName {  get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName {  get; set; }

        public decimal PriceSale { get; set; }
        public bool IsLockedSale { get; set; }

        public decimal PricePurchase { get; set; }
        public bool IsLockedPurchase { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
