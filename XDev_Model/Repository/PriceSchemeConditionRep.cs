using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PriceSchemeConditionRep : GenericEntity<PriceSchemeCondition>, IPriceSchemeConditionRep
    {
        public PriceSchemeConditionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
