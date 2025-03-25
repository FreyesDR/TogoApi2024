using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Admin;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class UsersBL : IUsersBL
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IHttpContextAccessor contextAccessor;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public UsersBL(ApplicationDbContext dbContext, 
                        IHttpContextAccessor contextAccessor,
                        IMapper mapper,
                        UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.contextAccessor = contextAccessor;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task UpdateAsync(UserDTO dto)
        {
            var model = await userManager.FindByIdAsync(dto.Id);
            if (model is not null)
            {
                model.Name = dto.Name;
                model.PhoneNumber = dto.PhoneNumber;
                model.IDTypeId = dto.IDTypeId;
                model.IDNumber = dto.IDNumber;
                model.IDCode = dto.IDCode;
                model.Active = dto.Active;                                
                model.ConcurrencyStamp = dto.ConcurrencyStamp;

                // Actualizar datos del usuario
                await userManager.UpdateAsync(model);                
            }
        }

        public async Task DeleteAsync(string id)
        {
            var model = await userManager.FindByIdAsync(id);
            if (model is not null)
                await userManager.DeleteAsync(model);
        }

        public async Task<IdentityResult> CreateAsync(UserCreateDTO dto)
        {
            var user = await userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                user = new ApplicationUser
                {
                    Id = dto.Id,
                    Email = dto.Email,
                    Name = dto.Name,
                    UserName = dto.Email,                    
                    Active = dto.Active,
                    IDTypeId = dto.IDTypeId,
                    IDNumber = dto.IDNumber,
                    IDCode = dto.IDCode,    
                    PhoneNumber = dto.PhoneNumber,  
                    EmailConfirmed = true,
                };

                var res = await userManager.CreateAsync(user, dto.Password);

                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user,"user");                    
                }

                return res;
            }
            else throw new CustomTogoException($"El correo electrónico {user.Email} ya existe");
        }

        public async Task<UserDTO> GetUserByIdAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user is null)
                return new UserDTO();

            return mapper.Map<UserDTO>(user);   
        }

        public async Task<List<UserListDTO>> GetUsersListAsync(PaginationDTO pagination)
        {
            var query = dbContext.Users.AsNoTracking();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<ApplicationUser, UserListDTO>(pagination, contextAccessor.HttpContext);
        }
    }
}
