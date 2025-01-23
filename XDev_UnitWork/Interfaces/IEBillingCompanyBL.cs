using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Interfaces
{
    public interface IEBillingCompanyBL : IGenericBL<EBillingCompanyDTO>
    {
        Task<List<EBillingCompanyAddressDTO>> GetCompanyAddress(string companyId);
        Task<List<EBillingCompanyIDsDTO>> GetCompanyDocumentsIDs(string companyId);
        Task<List<EBillingCompanyListDTO>> GetListAsync(PaginationDTO pagination, string ebillingid);
    }
}
