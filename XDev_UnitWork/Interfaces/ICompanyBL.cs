using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface ICompanyBL : IGenericBL<CompanyDTO>
    {
        Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string companyid);
        Task<AddressDTO> GetAddressById(string companyid, string id);
        Task<CompanyDTO> GetByCode(string code);
        Task<List<CompanyListDTO>> GetCompanyListAsync();
        Task<List<CompanyListDTO>> GetCompanyListAsync(PaginationDTO pagination);
    }
}
