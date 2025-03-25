using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class RegimenExportRep : GenericEntity<RegimenExport>, IRegimenExportRep
    {
        public RegimenExportRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
