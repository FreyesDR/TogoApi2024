using System.ComponentModel.DataAnnotations.Schema;

namespace XDev_Model.Entities
{
    public class MaterialWareHouse:AuditEntity
    {            
        public Guid MaterialId { get; set; }
        public Guid BranchId { get; set; }                
        public Guid WareHouseId { get; set; }

        public decimal Stock { get; set; }        
        public decimal SoldStock { get; set; }        
        public decimal PurchasedStock { get; set; }
        public decimal LockedStock {  get; set; }
        public decimal InTransitStock {  get; set; }
    }
}
