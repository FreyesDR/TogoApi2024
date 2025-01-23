using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IAppLogBL
    {
        Task CreateAsync(AppLogDTO dto);
        Task<List<AppLogDTO>> GetListAsync(PaginationDTO pagination);
    }
}
