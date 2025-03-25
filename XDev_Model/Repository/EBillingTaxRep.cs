using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EBillingTaxRep : GenericEntity<EBillingTax>, IEBillingTaxRep
    {
        public EBillingTaxRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
