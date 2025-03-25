using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Interfaces
{
    public interface ICompanyBL : IGenericBL<CompanyDTO>
    {
        Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string companyid);
        Task<AddressDTO> GetAddressById(string companyid, string id);
        Task<CompanyDTO> GetByCode(string code);
        Task<CompanyInfoDTO> GetCompanyInfoAsync(Guid coid);
        Task<List<CompanyListDTO>> GetCompanyListAsync();
        Task<List<CompanyListDTO>> GetCompanyListAsync(PaginationDTO pagination);
    }
}
