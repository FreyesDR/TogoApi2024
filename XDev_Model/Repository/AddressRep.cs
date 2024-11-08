using XDev_Model.Entities;
using XDev_Model.Interfaces;

namespace XDev_Model.Repository
{
    public class AddressRep : GenericEntity<Address>, IAddressRep
    {
        public AddressRep(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
