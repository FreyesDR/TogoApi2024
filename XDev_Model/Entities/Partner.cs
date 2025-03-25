namespace XDev_Model.Entities
{
    public class Partner : AuditEntity
    {
        public Guid Id { get; set; }
        public Guid PartnerTypeId { get; set; }
        public PartnerType PartnerType { get; set; }
        public string Code { get; set; }
        public string OldCode { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonIDNumber { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonEmail { get; set; }
        public bool Active { get; set; }
        public Guid PaymentConditionId {  get; set; }
        public PaymentCondition PaymentCondition { get; set; }

        public HashSet<PartnerRoles> Roles { get; set; }
        public HashSet<Address> Addresses { get; set; }
        public HashSet<PartnerID> PartnerIDS { get; set; }
        public HashSet<PartnerEconomicActivity> EconomicActivities { get; set; }
        public HashSet<PartnerCompany> Companies { get; set; }
    }
}

