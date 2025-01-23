namespace XDev_Model.Entities
{
    public class EBilling:AuditEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlTest { get; set; }
        public string UrlProd { get; set; }
        public string UrlSigner { get; set; }

        public HashSet<EBillingCompany> Companies { get; set; }
    }
}
