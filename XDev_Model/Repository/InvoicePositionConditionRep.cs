using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class InvoicePositionConditionRep : GenericEntity<InvoicePositionCondition>, IInvoicePositionConditionRep
    {
        public InvoicePositionConditionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
