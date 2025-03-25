namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string UrlTest { get; set; }
        public string UrlProd { get; set; }
        public string UrlSigner { get; set; }
		public string CertPathTest { get; set; }
		public string CertPathProd { get; set; }
	}
}
