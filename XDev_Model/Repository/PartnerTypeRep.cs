using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerTypeRep : GenericEntity<PartnerType>, IPartnerTypeRep
    {
        public PartnerTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
