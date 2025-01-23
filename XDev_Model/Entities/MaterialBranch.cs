namespace XDev_Model.Entities
{
    public class MaterialBranch:AuditEntity
    {        
        public Guid MaterialId { get; set; }          
        public Guid BranchId { get; set; }        

        public decimal PriceSale { get; set; }
        public bool IsLockedSale { get; set; }

        public decimal PricePurchase { get; set; }
        public bool IsLockedPurchase { get; set; }

        
    }
}
