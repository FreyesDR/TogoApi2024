using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IGenericBL<T>
    {
        Task<bool> AnyAsync(Guid id);
        Task<bool> AnyAsync(string code);
        Task DeleteAsync(Guid id);
        Task CreateAsync(T dto);
        Task UpdateAsync(T dto);
        Task<T> GetByIdAsync(params object[] ids);
        Task<List<T>> GetListAsync(PaginationDTO pagination);
        Task<List<T>> GetListAsync();        
    }
}
