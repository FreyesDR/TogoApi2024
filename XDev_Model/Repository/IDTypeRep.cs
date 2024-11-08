using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class IDTypeRep : GenericEntity<IDType>, IIDTypeRep
    {
        public IDTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
