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
    public class RegionBL : GenericBL<IRegionRep>, IRegionBL
    {
        public RegionBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(RegionDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code && f.CountryId == dto.CountryId);
            if (model is null)
            {
                model = Mapper.Map<Region>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"Código región [{model.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<RegionDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids[0]);
            if (model is null)
                return new RegionDTO { CountryId = Guid.Parse(ids[1].ToString()) };

            return Mapper.Map<RegionDTO>(model);
        }

        public Task<List<RegionDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegionDTO>> GetListAsync(PaginationDTO pagination, Guid countryid)
        {
            var query = await Repository.QueryAsync(f => f.CountryId == countryid);
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<Region, RegionDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<RegionDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegionDTO>> GetListAsync(Guid countryid)
        {
            var list = await Repository.GetListAsync(f => f.CountryId == countryid);
            return Mapper.Map<List<RegionDTO>>(list.Select(s => new RegionDTO
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList());
        }

        public async Task UpdateAsync(RegionDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;

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
