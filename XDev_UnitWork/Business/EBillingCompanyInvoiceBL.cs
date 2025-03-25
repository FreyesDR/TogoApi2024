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
    public class EBillingCompanyInvoiceBL : GenericBL<IEBillingCompanyInvoiceRep>, IEBillingCompanyInvoiceBL
    {
        public EBillingCompanyInvoiceBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(EBillingCompanyInvoiceDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId && f.InvoiceTypeId == dto.InvoiceTypeId);
            if (model is null)
            {
                model = Mapper.Map<EBillingCompanyInvoice>(dto);
                model.EBillingCompany = null;

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Tipo Factura ya existe para esta Sociedad");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EBillingCompanyInvoiceDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new EBillingCompanyInvoiceDTO()
                {
                    EBillingId = ids[0].ToString().GetGuid(),
                    CompanyId = ids[1].ToString().GetGuid(),
                    InvoiceTypeId = ids[2].ToString().GetGuid(),
                };

            return Mapper.Map<EBillingCompanyInvoiceDTO>(model);
        }

        public Task<List<EBillingCompanyInvoiceDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingCompanyInvoiceListDTO>> GetListAsync(PaginationDTO pagination, string companyid)
        {
            var query = (from coin in DbContext.EBillingCompanyInvoice.AsNoTracking()
                         join ebdo in DbContext.EBillingDocument.AsNoTracking() on coin.EBillingDocumentId equals ebdo.Id
                         join invt in DbContext.InvoiceType.AsNoTracking() on coin.InvoiceTypeId equals invt.Id
                         where coin.CompanyId == companyid.GetGuid()
                         select new EBillingCompanyInvoiceListDTO
                         {                             
                             InvoiceTypeId = coin.InvoiceTypeId,
                             InvoiceTypeName = invt.Name,
                             EBillingDocumentId = coin.EBillingDocumentId,
                             EBillingDocumentName = ebdo.Name,
                             RangeStart = coin.RangeStart,
                             RangeEnd = coin.RangeEnd,
                             Current = coin.Current,
                             ReStartYear = coin.ReStartYear,
                             NextReStart = coin.NextReStart,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<EBillingCompanyInvoiceListDTO, EBillingCompanyInvoiceListDTO>(pagination,ContextAccessor.HttpContext);
        }

        public Task<List<EBillingCompanyInvoiceDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EBillingCompanyInvoiceDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId && f.InvoiceTypeId == dto.InvoiceTypeId);
            try
            {
                if(model is not null)
                {
                    model.EBillingDocumentId = dto.EBillingDocumentId;
                    model.RangeStart = dto.RangeStart;
                    model.RangeEnd = dto.RangeEnd;
                    model.Current = dto.Current;
                    model.ReStartYear = dto.ReStartYear;
                    model.NextReStart = dto.NextReStart;

                    await Repository.UpdateAsync(model,dto.ConcurrencyStamp);
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
