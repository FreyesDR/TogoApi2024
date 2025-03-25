using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Interfaces
{
    public interface IBranchBL:IGenericBL<BranchDTO>
    {
        Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string branchid);
        Task<AddressDTO> GetAddressById(string branchid, string id);
        Task<BranchInfoDTO> GetBranchInfoAsync(Guid branchid);
        Task<List<BranchListDTO>> GetBranchListAsync(PaginationDTO pagination, string companyid);
        Task<List<BranchListDTO>> GetListAsync(string companyid);
    }
}
