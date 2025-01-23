using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class UnitMeasureRep : GenericEntity<UnitMeasure>, IUnitMeasureRep
    {
        public UnitMeasureRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
