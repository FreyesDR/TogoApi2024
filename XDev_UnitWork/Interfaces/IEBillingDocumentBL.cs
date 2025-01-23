using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Interfaces
{
    public interface IEBillingDocumentBL : IGenericBL<EBillingDocumentDTO>
    {
        Task<List<EBillingDocumentDTO>> GetListAsync(PaginationDTO pagination, string ebillingid);
        Task<List<EBillingDocumentDTO>> GetListAsync(string ebillingid);
    }
}
