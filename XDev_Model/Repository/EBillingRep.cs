using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EBillingRep : GenericEntity<EBilling>, IEBillingRep
    {
        public EBillingRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
