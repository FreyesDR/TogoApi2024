namespace XDev_Model.Entities
{
    public class Branch: AuditEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyId { get; set; }
        public Company Company { get; set; }
        public Guid BranchTypeId { get; set; }
        public BranchType BranchType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Address> Addresses { get; set; }
        public HashSet<WareHouse> WareHouses { get; set; }
        public HashSet<PointSale> PointsSale { get; set; }
    }
}
