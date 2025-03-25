using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class InvoiceRep : GenericEntity<Invoice>, IInvoiceRep
    {
        public InvoiceRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
