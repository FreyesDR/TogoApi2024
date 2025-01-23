using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class MaterialBranchRep : GenericEntity<MaterialBranch>, IMaterialBranchRep
    {
        public MaterialBranchRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
