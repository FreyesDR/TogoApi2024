using Microsoft.AspNetCore.Identity;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IAccountBL
    {
        Task<IdentityResult> ChangePassword(ChangePasswordDTO dto);
        Task<UserInfoDTO> UserInfo();
    }
}
