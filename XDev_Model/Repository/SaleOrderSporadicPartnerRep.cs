using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class SaleOrderSporadicPartnerRep : GenericEntity<SaleOrderSporadicPartner>, ISaleOrderSporadicPartnerRep
    {
        public SaleOrderSporadicPartnerRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
