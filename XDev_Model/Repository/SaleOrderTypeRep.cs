using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class SaleOrderTypeRep : GenericEntity<SaleOrderType>, ISaleOrderTypeRep
    {
        public SaleOrderTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
