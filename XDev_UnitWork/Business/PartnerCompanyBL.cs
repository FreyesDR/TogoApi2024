using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Business
{
    public class PartnerCompanyBL : GenericBL<IPartnerCompanyRep>, IPartnerCompanyBL
    {
        public PartnerCompanyBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(PartnerCompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == dto.PartnerId && f.CompanyId == dto.CompanyId);
            if (model is null)
            {
                model = Mapper.Map<PartnerCompany>(dto);

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Sociedad ya existe");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Guid partnerid, Guid companyid)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == partnerid && f.CompanyId == companyid);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public Task<PartnerCompanyDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerCompanyDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<PartnerCompanyDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerCompanyDTO>> GetListAsync(Guid partnerid)
        {
            return await (from pc in DbContext.PartnerCompany.AsNoTracking()
                          join co in DbContext.Company.AsNoTracking() on pc.CompanyId equals co.Id
                          where pc.PartnerId == partnerid
                          select new PartnerCompanyDTO
                          {
                              Id = pc.CompanyId,
                              PartnerId = pc.PartnerId,
                              CompanyId = pc.CompanyId,
                              CompanyCode = co.Code,
                              CompanyName = co.Name,
                              TradeName = co.TradeName
                          }).ToListAsync();
        }

        public Task UpdateAsync(PartnerCompanyDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
