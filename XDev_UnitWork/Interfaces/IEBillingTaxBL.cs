using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Interfaces
{
    public interface IEBillingTaxBL : IGenericBL<EBillingTaxDTO>
    {
        Task<List<EBillingTaxDTO>> GetListAsync(PaginationDTO pagination, string ebillingid);
    }
}
