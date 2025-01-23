using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PriceConditionRep : GenericEntity<PriceCondition>, IPriceConditionRep
    {
        public PriceConditionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
