using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class CurrencyRep : GenericEntity<Currency>, ICurrencyRep
    {
        public CurrencyRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
