using AutoMapper;
using XDev_Model.Entities;
using XDev_Model;
using XDev_Model.Interfaces;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;

namespace XDev_UnitWork.Business
{
    public class PartnerTypeBL:GenericBL<IPartnerTypeRep>, IPartnerTypeBL
    {
        public PartnerTypeBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public Task CreateAsync(PartnerTypeDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PartnerTypeDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerTypeDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PartnerType, PartnerTypeDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<PartnerTypeDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync();
            return Mapper.Map<List<PartnerTypeDTO>>(list.Select(s => new PartnerTypeDTO
            {
                Id = s.Id,
                Name = s.Name,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList());
        }

        public Task UpdateAsync(PartnerTypeDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
