using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Business
{
    public class PartnerEconomicActivityBL : GenericBL<IPartnerEconomicActivityRep>, IPartnerEconomicActivityBL
    {
        public PartnerEconomicActivityBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(PartnerEconomicActivityDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == dto.PartnerId && f.EconomicActivityId == dto.EconomicActivityId);
            if (model is null)
            {
                model = Mapper.Map<PartnerEconomicActivity>(dto);

                model.EconomicActivity = null;

                if (dto.Principal)
                {
                    await DbContext.Database.ExecuteSqlAsync($"UPDATE PartnerEconomicActivity SET PRINCIPAL = 0 WHERE PARTNERID={dto.PartnerId}");
                }

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Actividad económica ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<PartnerEconomicActivityDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new PartnerEconomicActivityDTO { PartnerId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<PartnerEconomicActivityDTO>(model);
        }

        public Task<List<PartnerEconomicActivityDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerEconomicActivityDTO>> GetListAsync(PaginationDTO pagination, string partnerid)
        {
            var query = (from pea in DbContext.PartnerEconomicActivity.AsNoTracking()
                         join ea in DbContext.EconomicActivities.AsNoTracking() on pea.EconomicActivityId equals ea.Id
                         where pea.PartnerId == partnerid.GetGuid()
                         select new PartnerEconomicActivityDTO
                         {
                             Id = pea.Id,
                             EconomicActivityName = ea.Name,
                             EconomicActivityCode = ea.Code,
                             Principal = pea.Principal
                         });

            query = query.CreateFilterAndOrder(pagination);
            var ret = await query.CreatePaging<PartnerEconomicActivityDTO, PartnerEconomicActivityDTO>(pagination, ContextAccessor.HttpContext);
            return ret;
        }

        public Task<List<PartnerEconomicActivityDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PartnerEconomicActivityDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Principal = dto.Principal;

                    if (dto.Principal)
                    {
                        await DbContext.Database.ExecuteSqlAsync($"UPDATE PA006 SET PRINCIPAL = 0 WHERE PARTNERID={dto.PartnerId}");
                    }

                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }
    }
}
