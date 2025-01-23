using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class EBillingDocumentRep : GenericEntity<EBillingDocument>, IEBillingDocumentRep
    {
        public EBillingDocumentRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
