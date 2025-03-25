using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop.Infrastructure;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.PriceScheme;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class PriceSchemeBL : GenericBL<IPriceSchemeRep>, IPriceSchemeBL
    {
        public PriceSchemeBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await Repository.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> AnyAsync(string code)
        {
            return await Repository.AnyAsync(f => f.Code == code);
        }

        public async Task CreateAsync(PriceSchemeDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                for (int i = 0; i < dto.Conditions.Count; i++)
                {
                    dto.Conditions[i].Position = Convert.ToInt16(i);
                }

                model = Mapper.Map<PriceScheme>(dto);                
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"El código [{dto.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<PriceSchemeDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == Guid.Parse(ids[0].ToString()), 
                                                                include: s => s.Include(i => i.Conditions)
                                                                               .ThenInclude(t => t.PriceCondition));
            if (model is null)
                return new PriceSchemeDTO();

            model.Conditions = model.Conditions.OrderBy(o => o.Position).ToHashSet();

            return Mapper.Map<PriceSchemeDTO>(model);
        }

        public Task<List<PriceSchemeDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriceSchemeListDTO>> GetPriceSchemeListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PriceScheme, PriceSchemeListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<PriceSchemeDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PriceSchemeListDTO>> GetPriceSchemeListAsync()
        {
            var list = await Repository.GetListAsync();
            return Mapper.Map<List<PriceSchemeListDTO>>(list.Select(s => new PriceSchemeListDTO
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
            }).OrderBy(o => o.Code).ToList());
        }

        public async Task UpdateAsync(PriceSchemeDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id, s => s.Include(i => i.Conditions));
            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;
                    for (int i = 0; i < dto.Conditions.Count; i++)
                    {
                        var exists = model.Conditions.FirstOrDefault(f => f.Id == dto.Conditions[i].Id);
                        if (exists is not null)
                            exists.Position = Convert.ToInt16(i);   
                        
                        if(exists is null)
                            dto.Conditions[i].Position = Convert.ToInt16(i);

                    }
                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);

                    var news = Mapper.Map<List<PriceSchemeCondition>>(dto.Conditions.Where(w => model.Conditions.All(f => f.Id != w.Id)));
                    await DbContext.PriceSchemeCondition.AddRangeAsync(news);

                    var remove = model.Conditions.Where(w => dto.Conditions.All(f => f.Id != w.Id)).ToList();
                    DbContext.PriceSchemeCondition.RemoveRange(remove);
                    await DbContext.SaveChangesAsync();
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
