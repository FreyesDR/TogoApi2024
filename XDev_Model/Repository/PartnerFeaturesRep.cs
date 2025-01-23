using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PartnerFeaturesRep : GenericEntity<PartnerFeatures>, IPartnerFeaturesRep
    {
        public PartnerFeaturesRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
