using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.Interfaces
{
    public interface ICityBL:IGenericBL<CityDTO>
    {
        Task<List<CityDTO>> GetListAsync(Guid regionid);
        Task<List<CityDTO>> GetListAsync(PaginationDTO pagination, Guid regionid);
    }
}
