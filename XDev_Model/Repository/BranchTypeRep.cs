using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class BranchTypeRep : GenericEntity<BranchType>, IBranchTypeRep
    {
        public BranchTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
