using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EBillingCompanyInvoiceRep : GenericEntity<EBillingCompanyInvoice>, IEBillingCompanyInvoiceRep
    {
        public EBillingCompanyInvoiceRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
