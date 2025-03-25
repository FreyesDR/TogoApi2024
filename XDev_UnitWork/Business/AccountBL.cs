

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class AccountBL : IAccountBL
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;

        public AccountBL(UserManager<ApplicationUser> usermanager, IHttpContextAccessor contextAccessor, IMapper mapper, ApplicationDbContext dbContext)
        {
            userManager = usermanager;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordDTO dto)
        {
            var user = await userManager.FindByEmailAsync(UtilsExtension.GetCurrentUserEmail(contextAccessor));

            if (user is null)
                throw new CustomTogoException("Datos incorrectos");

            return await userManager.ChangePasswordAsync(user, dto.OldPassword, dto.NewPassword);

        }

        public async Task<UserInfoDTO> UserInfo()
        {
            var user = await userManager.FindByIdAsync(UtilsExtension.GetCurrentUserId(contextAccessor));

            var userI = mapper.Map<UserInfoDTO>(user);

            if (user is not null)
            {
                userI.Roles = await userManager.GetRolesAsync(user);
            }

            var idtype = await dbContext.IDType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == user.IDTypeId);
            if (idtype is not null)
                userI.IDType = idtype.Name;

            return userI;
        }
    }
}
