using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CountryRep : GenericEntity<Country>, ICountryRep
    {
        public CountryRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
