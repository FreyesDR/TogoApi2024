using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class MaterialTypeRep : GenericEntity<MaterialType>, IMaterialTypeRep
    {
        public MaterialTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
