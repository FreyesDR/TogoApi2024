using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.SaleOrder;

namespace XDev_UnitWork.Interfaces
{
    public interface ISaleOrderBL : IGenericBL<SaleOrderDTO>
    {
        Task<MemoryStream> GetPdfFormAsync(Guid id);
        Task<List<SaleOrderListDTO>> GetSOListAsync(PaginationDTO pagination);
    }
}
