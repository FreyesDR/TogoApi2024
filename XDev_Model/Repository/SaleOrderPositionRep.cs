using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class SaleOrderPositionRep : GenericEntity<SaleOrderPosition>, ISaleOrderPositionRep
    {
        public SaleOrderPositionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
