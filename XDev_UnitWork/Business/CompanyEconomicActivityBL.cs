using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Business
{
    public class CompanyEconomicActivityBL : GenericBL<ICompanyEconomicActivityRep>, ICompanyEconomicActivityBL
    {
        public CompanyEconomicActivityBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(CompanyEconomicActivityDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId && f.EconomicActivityId == dto.EconomicActivityId);
            if (model is null)
            {
                model = Mapper.Map<CompanyEconomicActivity>(dto);
                model.Company = null;
                model.EconomicActivity = null;

                if (dto.Principal)
                {
                    await DbContext.Database.ExecuteSqlAsync($"UPDATE CompanyEconomicActivities SET PRINCIPAL = 0 WHERE COMPANYID={dto.CompanyId}");
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

        public async Task<CompanyEconomicActivityDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new CompanyEconomicActivityDTO { CompanyId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<CompanyEconomicActivityDTO>(model);
        }

        public Task<List<CompanyEconomicActivityDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CompanyEconomicActivityDTO>> GetListAsync(PaginationDTO pagination, string companyid)
        {
            var query = (from cea in DbContext.CompanyEconomicActivities.AsNoTracking()
                         join ea in DbContext.EconomicActivities.AsNoTracking() on cea.EconomicActivityId equals ea.Id
                         where cea.CompanyId == companyid.GetGuid()
                         select new CompanyEconomicActivityDTO
                         {
                             Id = cea.Id,
                             EconomicActivityName = ea.Name,
                             EconomicActivityCode = ea.Code,
                             Principal = cea.Principal
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<CompanyEconomicActivityDTO, CompanyEconomicActivityDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<CompanyEconomicActivityDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CompanyEconomicActivityDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Principal = dto.Principal;

                    if (dto.Principal)
                    {
                        await DbContext.Database.ExecuteSqlAsync($"UPDATE CO007 SET PRINCIPAL = 0 WHERE COMPANYID={dto.CompanyId}");
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
