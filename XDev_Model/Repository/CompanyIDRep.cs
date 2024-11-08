using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CompanyIDRep : GenericEntity<CompanyID>, ICompanyIDRep
    {
        public CompanyIDRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
