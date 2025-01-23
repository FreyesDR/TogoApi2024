using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.Business
{
    public class CountryBL : GenericBL<ICountryRep>, ICountryBL
    {
        public CountryBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(CountryDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                model = Mapper.Map<Country>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"Código país [{model.Code}] ya existe");

        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<CountryDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new CountryDTO();

            return Mapper.Map<CountryDTO>(model);
        }

        public async Task<List<CountryDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<Country, CountryDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<CountryDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync();
            return Mapper.Map<List<CountryDTO>>(list.Select(s => new CountryDTO
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList());
        }

        public async Task UpdateAsync(CountryDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;
                    model.CodeMH = dto.CodeMH;

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
