using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.ElectronicBilling
{
    public class EBillingCompanyDTO : AuditEntityDTO
    {
        public Guid EBillingId { get; set; }
        public Guid CompanyId { get; set; }
        public bool IsProd { get; set; }
        public string ApiUser { get; set; }
        public string ApiKeyTest { get; set; }
        public string ApiKeyProd { get; set; }
        public string PrivateKeyTest { get; set; }
        public string PrivateKeyProd { get; set; }
        public bool Active { get; set; }
        public bool Contingency { get; set; }
        public string SmtpService { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpUserPassword { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string FromName { get; set; }
        public string CcEmail1 { get; set; }
        public string CcEmail2
        {
            get; set;

        }        

    }

    public class EBillingCompanyListDTO
    {
        public Guid EBillingId { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public bool IsProd { get; set; }
        public bool Active { get; set; }
        public bool Contingency { get; set; }
    }

    public class EBillinCertsDTO
    {
        public IFormFile FileTest { get; set; }
        public IFormFile FilePrd { get; set; }
        public string CompanyId { get; set; }
    }
}
