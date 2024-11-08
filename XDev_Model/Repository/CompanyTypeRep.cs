using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CompanyTypeRep : GenericEntity<CompanyType>, ICompanyTypeRep
    {
        public CompanyTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
