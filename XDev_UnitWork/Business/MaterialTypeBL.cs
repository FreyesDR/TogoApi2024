using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class MaterialTypeBL : GenericBL<IMaterialTypeRep>, IMaterialTypeBL
    {
        public MaterialTypeBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(MaterialTypeDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                model = Mapper.Map<MaterialType>(dto);
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

        public async Task<MaterialTypeDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new MaterialTypeDTO();

            return Mapper.Map<MaterialTypeDTO>(model);
        }

        public async Task<List<MaterialTypeDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<MaterialType, MaterialTypeDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<MaterialTypeDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync();
            return Mapper.Map<List<MaterialTypeDTO>>(list.Select(s => new MaterialTypeDTO
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList());
        }

        public async Task UpdateAsync(MaterialTypeDTO dto)
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
