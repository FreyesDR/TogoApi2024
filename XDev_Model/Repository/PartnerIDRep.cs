using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerIDRep : GenericEntity<PartnerID>, IPartnerIDRep
    {
        public PartnerIDRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
