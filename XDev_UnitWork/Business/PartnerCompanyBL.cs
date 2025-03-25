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
                model.Partner = null;
                model.Company = null;

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

        public async Task<List<PartnerListDTO>> GetPartnerCompanyListAsync(PaginationDTO pagination, string companyid)
        {
            var patype = DbContext.PartnerRole.AsNoTracking().FirstOrDefault(f => f.Code == "D");

            var query = (from p in DbContext.Partner.AsNoTracking()
                         join pt in DbContext.PartnerType.AsNoTracking() on p.PartnerTypeId equals pt.Id
                         join pr in DbContext.PartnerRoles.AsNoTracking() on p.Id equals pr.PartnerId
                         join cop in DbContext.PartnerCompany.AsNoTracking() on p.Id equals cop.PartnerId
                         where cop.CompanyId == companyid.GetGuid() && pr.RoleId == patype.Id
                         select new PartnerListDTO
                         {
                             Id = p.Id,
                             Code = p.Code,
                             OldCode = p.OldCode,
                             Name = p.Name,
                             TradeName = p.TradeName,
                             Active = p.Active,
                             PartnerType = pt.Name,
                             PaymentConditionId = p.PaymentConditionId,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PartnerListDTO, PartnerListDTO>(pagination, ContextAccessor.HttpContext);
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
