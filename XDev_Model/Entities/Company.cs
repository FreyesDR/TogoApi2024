using System.ComponentModel.Design;

namespace XDev_Model.Entities
{
    public class Company: AuditEntity
    {
        public Guid Id { get; set; }
        public Guid CompanyTypeId { get; set; }
        public CompanyType CompanyType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public bool Active { get; set; }
        public string UrlLogo {  get; set; }
        public HashSet<Address> Addresses { get; set; }
        public HashSet<Branch> Branches { get; set; }
        public HashSet<CompanyID> CompanyIDS { get; set; }
        public HashSet<CompanyEconomicActivity> CompanyEconomicActivities { get; set; }
        public HashSet<PartnerCompany> Partners { get; set; }
    }
}
