using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerRolesRep : GenericEntity<PartnerRoles>,IPartnerRolesRep
    {
        public PartnerRolesRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
