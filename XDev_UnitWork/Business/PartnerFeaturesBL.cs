using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Business
{
    public class PartnerFeaturesBL : GenericBL<IPartnerFeaturesRep>, IPartnerFeaturesBL
    {
        public PartnerFeaturesBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public Task CreateAsync(PartnerFeaturesDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PartnerFeaturesDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public async Task<PartnerFeaturesDTO> GetAsync()
        {
            var model = await Repository.GetFirstorDefaultAsync();
            return Mapper.Map<PartnerFeaturesDTO>(model);
        }

        public Task<List<PartnerFeaturesDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerFeaturesDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PartnerFeaturesDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.NumType = dto.NumType;
                    model.RangeId = dto.RangeId;

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
