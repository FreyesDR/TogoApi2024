using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class InvoiceTypeRep : GenericEntity<InvoiceType>, IInvoiceTypeRep
    {
        public InvoiceTypeRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
