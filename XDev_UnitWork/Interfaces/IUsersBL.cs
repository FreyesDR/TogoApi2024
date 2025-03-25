using Microsoft.AspNetCore.Identity;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Admin;

namespace XDev_UnitWork.Interfaces
{
    public interface IUsersBL
    {
        Task<IdentityResult> CreateAsync(UserCreateDTO dto);
        Task DeleteAsync(string id);
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<List<UserListDTO>> GetUsersListAsync(PaginationDTO pagination);
        Task UpdateAsync(UserDTO dto);
    }
}
