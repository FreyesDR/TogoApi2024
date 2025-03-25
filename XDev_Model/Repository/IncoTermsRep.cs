using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class IncoTermsRep : GenericEntity<IncoTerms>, IIncoTermsRep
    {
        public IncoTermsRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
