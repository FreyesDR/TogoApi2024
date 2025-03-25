using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class InvoiceSporadicPartnerRep : GenericEntity<InvoiceSporadicPartner>, IInvoiceSporadicPartnerRep
    {
        public InvoiceSporadicPartnerRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
