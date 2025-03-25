using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IDashBoardBL
    {
        Task<DashBoardDTO> GetDashboardDataAsync();
        Task<DashBoardSaleDTO> GetSales(int days);
    }
}
