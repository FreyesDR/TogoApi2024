namespace XDev_Model.Entities
{
    public class IDType : AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string AltCode { get; set; }
        public string Name { get; set; }
        public HashSet<CompanyID> CompanyIDS { get; set; }
        public HashSet<PartnerID> PartnerIDS { get; set; }
    }
}
