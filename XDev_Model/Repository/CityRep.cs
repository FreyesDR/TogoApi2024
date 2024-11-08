using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CityRep : GenericEntity<City>, ICityRep
    {
        public CityRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
