namespace XDev_Model.Entities
{
    public class BranchType : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Branch> Branches { get; set; }
    }
}
