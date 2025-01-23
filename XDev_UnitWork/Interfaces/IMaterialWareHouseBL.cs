using XDev_UnitWork.DTO.Material;

namespace XDev_UnitWork.Interfaces
{
    public interface IMaterialWareHouseBL : IGenericBL<MaterialWareHouseDTO>
    {
        Task<List<MaterialWareHouseListDTO>> GetListAsync(string materialid, string branchid);
    }
}
