using Microsoft.AspNetCore.Http.HttpResults;
using XDev_Model.Entities;
using XDev_UnitWork.Business;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.FeSv;

namespace XDev_UnitWork.Interfaces
{
    public interface IFeSvBL
    {
        Task<FeResponseDTO> ProcessCancelInvoiceAsync(Invoice invoice, FeCancelDTO dto);
        Task<FeResponseDTO> ProcessInvoiceAsync(Invoice invoice);
    }
}
