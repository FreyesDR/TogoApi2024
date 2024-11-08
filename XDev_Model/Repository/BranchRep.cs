using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class BranchRep : GenericEntity<Branch>, IBranchRep
    {
        public BranchRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
