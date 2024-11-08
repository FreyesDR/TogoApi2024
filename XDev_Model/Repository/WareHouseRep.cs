using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class WareHouseRep : GenericEntity<WareHouse>, IWareHouseRep
    {
        public WareHouseRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
