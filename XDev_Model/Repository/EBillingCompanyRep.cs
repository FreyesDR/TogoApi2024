using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EBillingCompanyRep : GenericEntity<EBillingCompany>, IEBillingCompanyRep
    {
        public EBillingCompanyRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
