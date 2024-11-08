using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IWareHouseBL : IGenericBL<WareHouseDTO>
    {
        Task<List<WareHouseDTO>> GetWareHouseListAsync(PaginationDTO pagination, string branchid);
    }
}
