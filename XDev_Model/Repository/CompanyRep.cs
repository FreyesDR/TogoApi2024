using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CompanyRep : GenericEntity<Company>, ICompanyRep
    {
        public CompanyRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
