using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EndPointPolicyRep : GenericEntity<EndPointPolicy>, IEndPointPolicyRep
    {
        public EndPointPolicyRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
