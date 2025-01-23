using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class NumberRangeRep : GenericEntity<NumberRange>, INumberRangeRep
    {
        public NumberRangeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
