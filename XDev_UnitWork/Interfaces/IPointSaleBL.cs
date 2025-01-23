using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Interfaces
{
    public interface IPointSaleBL : IGenericBL<PointSaleDTO>
    {
        Task<List<PointSaleDTO>> GetListAsync(string branchid);
        Task<List<PointSaleDTO>> GetPointSaleListAsync(PaginationDTO pagination, string branchid);
    }
}
