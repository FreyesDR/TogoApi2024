using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class InvoicePositionRep : GenericEntity<InvoicePosition>, IInvoicePositionRep
    {
        public InvoicePositionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
