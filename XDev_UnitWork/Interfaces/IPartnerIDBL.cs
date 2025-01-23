using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerIDBL : IGenericBL<PartnerIDDTO>
    {
        Task<List<PartnerIDDTO>> GetListAsync(PaginationDTO pagination, string partnerid);
    }
}
