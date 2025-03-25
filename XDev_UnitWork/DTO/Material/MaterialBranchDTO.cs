using Microsoft.EntityFrameworkCore;
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
        public string BranchCode {  get; set; }
        public string BranchName {  get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName {  get; set; }
        [Precision(18, 2)]
        public decimal PriceSale { get; set; }
        public bool IsLockedSale { get; set; }
        [Precision(18, 2)]
        public decimal PricePurchase { get; set; }
        public bool IsLockedPurchase { get; set; }
        [Precision(18,3)]
        public decimal Stock { get; set; }
        [Precision(18, 3)]
        public decimal SoldStock { get; set; }
        [Precision(18, 3)]
        public decimal PurchasedStock { get; set; }
        [Precision(18, 3)]
        public decimal LockedStock { get; set; }
        [Precision(18, 3)]
        public decimal InTransitStock { get; set; }
        public string ConcurrencyStamp { get; set; }
    }
}
