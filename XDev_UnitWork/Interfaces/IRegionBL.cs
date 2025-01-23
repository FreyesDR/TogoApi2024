using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.Interfaces
{
    public interface IRegionBL:IGenericBL<RegionDTO>
    {
        Task<List<RegionDTO>> GetListAsync(PaginationDTO pagination, Guid countryid);
        Task<List<RegionDTO>> GetListAsync(Guid countryid);
    }
}
