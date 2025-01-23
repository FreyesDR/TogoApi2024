
using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class SaleOrderRep : GenericEntity<SaleOrder>, ISaleOrderRep
    {
        public SaleOrderRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
