using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class MeanOfPaymentRep : GenericEntity<MeanOfPayment>, IMeanOfPaymentRep
    {
        public MeanOfPaymentRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
