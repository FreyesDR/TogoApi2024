using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class EBillingBL : GenericBL<IEBillingRep>, IEBillingBL
    {
        public EBillingBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public Task CreateAsync(EBillingDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EBillingDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new EBillingDTO();

            return Mapper.Map<EBillingDTO>(model);
        }

        public async Task<List<EBillingDTO>> GetListAsync(PaginationDTO pagination)
        {
            var query = await Repository.QueryAsync();
            var list = query.Select(s => new EBillingDTO { Id = s.Id, Name = s.Name }).AsQueryable();
            list = list.CreateFilterAndOrder(pagination);
            return await list.CreatePaging<EBillingDTO, EBillingDTO>(pagination, ContextAccessor.HttpContext);

        }

        public Task<List<EBillingDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EBillingDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;
                    model.UrlTest = dto.UrlTest;
                    model.UrlSigner = dto.UrlSigner;
                    model.UrlProd = dto.UrlProd;
                    model.CertPathTest = dto.CertPathTest;
                    model.CertPathProd = dto.CertPathProd;

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
