using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerRep : GenericEntity<Partner>, IPartnerRep
    {
        public PartnerRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
