using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class RegionRep : GenericEntity<Region>, IRegionRep
    {
        public RegionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
