using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IBranchBL:IGenericBL<BranchDTO>
    {
        Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string branchid);
        Task<AddressDTO> GetAddressById(string branchid, string id);
        Task<List<BranchListDTO>> GetBranchListAsync(PaginationDTO pagination, string companyid);
    }
}
