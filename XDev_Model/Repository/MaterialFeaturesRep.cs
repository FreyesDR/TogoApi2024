using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class MaterialFeaturesRep : GenericEntity<MaterialFeatures>, IMaterialFeaturesRep
    {
        public MaterialFeaturesRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
