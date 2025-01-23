using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerRoleRep : GenericEntity<PartnerRole>, IPartnerRoleRep
    {
        public PartnerRoleRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
