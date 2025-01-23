using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class MaterialWareHouseRep : GenericEntity<MaterialWareHouse>, IMaterialWareHouseRep
    {
        public MaterialWareHouseRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
