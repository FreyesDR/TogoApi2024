
using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Material
{
    public class MaterialWareHouseDTO:AuditEntityDTO
    {
        public Guid MaterialId { get; set; }
        public Guid BranchId { get; set; }        
        public Guid WareHouseId { get; set; }

        public decimal Stock { get; set; }
        public decimal SoldStock { get; set; }
        public decimal PurchasedStock { get; set; }
        public decimal LockedStock { get; set; }
        public decimal InTransitStock { get; set; }        
    }

    public class MaterialWareHouseListDTO 
    {
        public Guid MaterialId { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName {  get; set; }
        public Guid WareHouseId { get; set; }
        public string WareHouseCode { get; set; }
        public string WareHouseName { get; set; }

        public decimal Stock { get; set; }
        public decimal SoldStock { get; set; }
        public decimal PurchasedStock { get; set; }
        public decimal LockedStock { get; set; }
        public decimal InTransitStock { get; set; }
    }
}
