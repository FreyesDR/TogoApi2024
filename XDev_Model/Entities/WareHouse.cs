namespace XDev_Model.Entities
{
    public class WareHouse : AuditEntity
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public Branch Branch { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
