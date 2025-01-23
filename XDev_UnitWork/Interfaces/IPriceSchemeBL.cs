using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.PriceScheme;

namespace XDev_UnitWork.Interfaces
{
    public interface IPriceSchemeBL : IGenericBL<PriceSchemeDTO>
    {
        Task<List<PriceSchemeListDTO>> GetPriceSchemeListAsync(PaginationDTO pagination);
        Task<List<PriceSchemeListDTO>> GetPriceSchemeListAsync();
    }
}
