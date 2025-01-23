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
    public class EBillingCompanyBL : GenericBL<IEBillingCompanyRep>, IEBillingCompanyBL
    {
        public EBillingCompanyBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(EBillingCompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId);
            if (model is null)
            {
                model = Mapper.Map<EBillingCompany>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Sociedad ya está registrada para Facturación Electrónica");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EBillingCompanyDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new EBillingCompanyDTO { EBillingId = ids[0].ToString().GetGuid() };

            return Mapper.Map<EBillingCompanyDTO>(model);
        }

        public Task<List<EBillingCompanyDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingCompanyListDTO>> GetListAsync(PaginationDTO pagination, string ebillingid)
        {
            var query = (from ebc in DbContext.EBillingCompany.AsNoTracking()
                         join co in DbContext.Company.AsNoTracking() on ebc.CompanyId equals co.Id
                         where ebc.EBillingId == ebillingid.GetGuid()
                         select new EBillingCompanyListDTO
                         {
                            EBillingId = ebc.EBillingId,
                             CompanyId = ebc.CompanyId,
                             CompanyCode = co.Code,
                             CompanyName = co.Name,
                             IsProd = ebc.IsProd,
                             Active = ebc.Active,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<EBillingCompanyListDTO, EBillingCompanyListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<EBillingCompanyDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EBillingCompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.EBillingId == dto.EBillingId && f.CompanyId == dto.CompanyId);
            try
            {
                if (model is not null)
                {
                    model.AddressId = dto.AddressId;
                    model.Nif1Id = dto.Nif1Id;
                    model.Nif2Id = dto.Nif2Id;
                    model.IsProd = dto.IsProd;
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

        public async Task<List<EBillingCompanyAddressDTO>> GetCompanyAddress(string companyId)
        {
            return await (from addr in DbContext.Address.AsNoTracking()
                          join addt in DbContext.AddressType.AsNoTracking() on addr.AddressTypeId equals addt.Id
                          where addr.CompanyId == companyId.GetGuid()
                          select new EBillingCompanyAddressDTO
                          {
                              AddressId = addr.Id,
                              AddressName = addt.Name,
                          }).ToListAsync();
        }

        public async Task<List<EBillingCompanyIDsDTO>> GetCompanyDocumentsIDs(string companyId)
        {
            return await (from coid in DbContext.CompanyID.AsNoTracking()
                          join idty in DbContext.IDType.AsNoTracking() on coid.IDTypeId equals idty.Id
                          where coid.CompanyId == companyId.GetGuid()
                          select new EBillingCompanyIDsDTO
                          {
                              DocumentId = coid.Id,
                              Document = $"{coid.DocumentNumber} - {idty.Name}"
                          }).ToListAsync();
        }
    }
}
