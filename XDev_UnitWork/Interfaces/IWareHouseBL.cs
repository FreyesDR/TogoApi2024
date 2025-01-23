using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Interfaces
{
    public interface IWareHouseBL : IGenericBL<WareHouseDTO>
    {
        Task<List<WareHouseDTO>> GetListAsync(string branchid);
        Task<List<WareHouseDTO>> GetWareHouseListAsync(PaginationDTO pagination, string branchid);
    }
}
