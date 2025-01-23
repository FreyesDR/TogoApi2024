namespace XDev_Model.Entities
{
    public class MaterialType: AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Material> Materials { get; set; } 
    }
}
