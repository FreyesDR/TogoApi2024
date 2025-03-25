using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Business
{
    public class PartnerRolesBL : GenericBL<IPartnerRolesRep>, IPartnerRolesBL
    {
        public PartnerRolesBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(PartnerRolesDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == dto.PartnerId && f.RoleId == dto.RoleId, i => i.Role);
            if (model is null)
            {
                model = Mapper.Map<PartnerRoles>(dto);
                model.Partner = null;
                model.Role = null;

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"Rol [{model.Role.Name}] ya fue asignado");
        }

        public async Task DeleteAsync(Guid partnerid, Guid roleid)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == partnerid && f.RoleId == roleid);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PartnerRolesDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerRolesDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerRolesDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerRolesDTO>> GetListAsync(Guid partnerId)
        {
            return await (from pr in DbContext.PartnerRoles.AsNoTracking()
                          join ro in DbContext.PartnerRole.AsNoTracking() on pr.RoleId equals ro.Id
                          where pr.PartnerId == partnerId
                          select new PartnerRolesDTO
                          {
                              Id = ro.Id,
                              PartnerId = pr.PartnerId,
                              RoleId = ro.Id,
                              RoleName = ro.Name,
                          }).ToListAsync();
        }

        public Task UpdateAsync(PartnerRolesDTO dto)
        {
            throw new NotImplementedException();
        }
        
    }
}
