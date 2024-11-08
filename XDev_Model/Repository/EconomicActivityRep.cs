using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EconomicActivityRep : GenericEntity<EconomicActivity>, IEconomicActivityRep
    {
        public EconomicActivityRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
