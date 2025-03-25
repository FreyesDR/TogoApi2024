using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Interfaces
{
    public interface IEBillingLogBL
    {
        Task<EBillingLogDTO> GetLogByIdAsync(Guid logId);
        Task<EBillingLogDTO> GetLogByInvoiceId(Guid id);
        Task<List<EBillingLogDTO>> GetPaginationAsync(PaginationDTO dto);
    }
}
