using Microsoft.AspNetCore.Http.HttpResults;
using XDev_Model.Entities;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.DTO.SaleOrder;

namespace XDev_UnitWork.Interfaces
{
    public interface IInvoiceBL
    {
        Task<bool> AnyByCodeGen(string codegen);
        Task<bool> AnyByNumber(string invnum);
        Task<FeResponseDTO> CancelInvoice(FeCancelDTO dto);
        Task<MemoryStream> CreateFormPDF(Guid id);
        Task<FeResponseDTO> CreateInvoiceAsync(SaleOrder dto);
        Task<Invoice> GetByAssignment(string assignment);
        Task<InvoiceDTO> GetByIdAsync(Guid id);
        Task<FeCancelDTO> GetInvoiceCancelAsync(string id);
        Task<List<InvoiceListDTO>> GetPaginationAsync(PaginationDTO pagination);
    }
}
