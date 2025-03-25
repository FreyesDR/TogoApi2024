using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class PaymentConditionRep : GenericEntity<PaymentCondition>, IPaymentConditionRep
    {
        public PaymentConditionRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
