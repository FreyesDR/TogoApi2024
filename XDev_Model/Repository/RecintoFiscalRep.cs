using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class RecintoFiscalRep : GenericEntity<RecintoFiscal>, IRecintoFiscalRep
    {
        public RecintoFiscalRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
