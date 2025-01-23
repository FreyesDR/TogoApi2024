using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Interfaces
{
    public interface ICompanyEconomicActivityBL : IGenericBL<CompanyEconomicActivityDTO>
    {
        Task<List<CompanyEconomicActivityDTO>> GetListAsync(PaginationDTO pagination, string companyid);
    }
}
