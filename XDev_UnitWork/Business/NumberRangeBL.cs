using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class NumberRangeBL : GenericBL<INumberRangeRep>, INumberRangeBL
    {
        public NumberRangeBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await Repository.AnyAsync(f => f.Id == id);
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(NumberRangeDTO dto)
        {
            var model = Mapper.Map<NumberRange>(dto);
            await Repository.CreateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<NumberRangeDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new NumberRangeDTO();

            return Mapper.Map<NumberRangeDTO>(model);
        }

        public async Task<List<NumberRangeDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<NumberRange, NumberRangeDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<NumberRangeDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync(f => f.Active == true);
            return Mapper.Map<List<NumberRangeDTO>>(list.Select(s => new NumberRangeDTO
            {
                Id = s.Id,
                Name = s.Name,
            }).OrderBy(o => o.Name).ToList());
        }

        public async Task UpdateAsync(NumberRangeDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;
                    model.NumStart = dto.NumStart;
                    model.NumEnd = dto.NumEnd;
                    model.NumCurrent = dto.NumCurrent;
                    model.Active = dto.Active;

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
