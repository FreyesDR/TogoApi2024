using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerCompanyRep : GenericEntity<PartnerCompany>, IPartnerCompanyRep
    {
        public PartnerCompanyRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
