using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerEconomicActivityBL : IGenericBL<PartnerEconomicActivityDTO>
    {
        Task<List<PartnerEconomicActivityDTO>> GetListAsync(PaginationDTO pagination, string partnerid);
    }
}
