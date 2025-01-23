using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PriceSchemeRep : GenericEntity<PriceScheme>, IPriceSchemeRep
    {
        public PriceSchemeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
