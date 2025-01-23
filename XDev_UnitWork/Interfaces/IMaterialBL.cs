using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Material;

namespace XDev_UnitWork.Interfaces
{
    public interface IMaterialBL : IGenericBL<MaterialDTO>
    {
        Task<MaterialSaleDTO> GetByCode(string code, string branchid);
        Task<List<MaterialSaleDTO>> GetMaterialActiveListAsync(PaginationDTO pagination, string branchid);
        Task<List<MaterialListDTO>> GetMaterialListAsync(PaginationDTO pagination);
    }
}
