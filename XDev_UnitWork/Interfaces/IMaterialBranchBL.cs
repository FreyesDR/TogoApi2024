using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Material;

namespace XDev_UnitWork.Interfaces
{
    public interface IMaterialBranchBL : IGenericBL<MaterialBranchDTO>
    {
        Task<List<MaterialBranchListDTO>> GetListAsync(string materialid);
    }
}
