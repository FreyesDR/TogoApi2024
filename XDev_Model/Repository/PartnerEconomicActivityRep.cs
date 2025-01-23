using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerEconomicActivityRep : GenericEntity<PartnerEconomicActivity>, IPartnerEconomicActivityRep
    {
        public PartnerEconomicActivityRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
