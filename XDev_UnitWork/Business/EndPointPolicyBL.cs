using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Admin;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class EndPointPolicyBL : GenericBL<IEndPointPolicyRep>, IEndPointPolicyBL
    {
        public EndPointPolicyBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public Task CreateAsync(EndPointPolicyDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EndPointPolicyDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<EndPointPolicyDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EndPointPolicyDTO>> GetListAsync()
        {
            var query = await (from ep in DbContext.EndPointPolicy.AsNoTracking()
                               join p in DbContext.Policy.AsNoTracking() on ep.PolicyId equals p.Id
                               select new EndPointPolicyDTO
                               {
                                   Id = ep.Id,
                                   MethodHttp = ep.MethodHttp,
                                   Description = ep.Description,
                                   Module = ep.Module,
                                   PolicyName = p.Name,
                                   PolicyParams = ep.PolicyParams,
                                   ConcurrencyStamp = ep.ConcurrencyStamp,
                               }).ToListAsync();

            return query;
        }

        public async Task UpdateAsync(EndPointPolicyDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Description = dto.Description;
                    model.PolicyParams = dto.PolicyParams;

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
