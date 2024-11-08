

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class AccountBL: IAccountBL
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;

        public AccountBL(UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            userManager = usermanager;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordDTO dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user is null)
                throw new Exception("Datos incorrectos");            

            return await userManager.ChangePasswordAsync(user,dto.OldPassword,dto.Password);
            
        }

        public async Task<UserInfoDTO> UserInfo()
        {
            var user = await userManager.FindByIdAsync(UtilsExtension.GetCurrentUserId(contextAccessor));

            var userI = mapper.Map<UserInfoDTO>(user);

            if (user is not null)
            {
                userI.Roles = await userManager.GetRolesAsync(user);
            }

            return userI;
        }
    }
}
