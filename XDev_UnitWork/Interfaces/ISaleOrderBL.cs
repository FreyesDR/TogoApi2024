using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.DTO.SaleOrder;

namespace XDev_UnitWork.Interfaces
{
    public interface ISaleOrderBL : IGenericBL<SaleOrderDTO>
    {
        Task<string> CreateSOAsync(SaleOrderDTO dto);
        Task<FeResponseDTO> CreateInvoiceAsync(string soid);
        Task<MemoryStream> GetPdfFormAsync(Guid id);
        Task<List<SaleOrderListDTO>> GetSOListAsync(PaginationDTO pagination);
    }
}
