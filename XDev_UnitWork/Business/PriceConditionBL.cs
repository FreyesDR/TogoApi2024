using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.PriceScheme;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class PriceConditionBL : GenericBL<IPriceConditionRep>, IPriceConditionBL
    {
        public PriceConditionBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(PriceConditionDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                model = Mapper.Map<PriceCondition>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"El código [{dto.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if(model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<PriceConditionDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if(model is null)
                return new PriceConditionDTO();

            return Mapper.Map<PriceConditionDTO>(model);
        }

        public async Task<List<PriceConditionDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PriceCondition, PriceConditionDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<PriceConditionDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync();
            return Mapper.Map<List<PriceConditionDTO>>(list);
        }

        public async Task UpdateAsync(PriceConditionDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                model.Name = dto.Name;
                model.Type = dto.Type;
                model.Source = dto.Source;
                model.Value = dto.Value;
                model.ValueType = dto.ValueType;
                model.Edit = dto.Edit;

                await Repository.UpdateAsync(model, dto.ConcurrencyStamp);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }
    }
}
