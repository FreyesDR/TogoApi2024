using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CompanyEconomicActivityRep : GenericEntity<CompanyEconomicActivity>, ICompanyEconomicActivityRep
    {
        public CompanyEconomicActivityRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
