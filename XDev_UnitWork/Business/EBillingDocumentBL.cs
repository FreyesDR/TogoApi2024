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
    public class EBillingDocumentBL : GenericBL<IEBillingDocumentRep>, IEBillingDocumentBL
    {
        public EBillingDocumentBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(EBillingDocumentDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code && f.EBillingId == dto.EBillingId);
            if (model is null)
            {
                model = Mapper.Map<EBillingDocument>(dto);
            }
            else throw new CustomTogoException($"El documento {dto.Code} ya existe");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EBillingDocumentDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if(model is null)
                return new EBillingDocumentDTO() { EBillingId = ids[0].ToString().GetGuid() };

            return Mapper.Map<EBillingDocumentDTO>(model);
        }

        public Task<List<EBillingDocumentDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingDocumentDTO>> GetListAsync(PaginationDTO pagination, string ebillingid)
        {
            var query = await Repository.QueryAsync(f => f.EBillingId == ebillingid.GetGuid());
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<EBillingDocument, EBillingDocumentDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<EBillingDocumentDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingDocumentDTO>> GetListAsync(string ebillingid)
        {
            var list = await Repository.GetListAsync(f => f.EBillingId == ebillingid.GetGuid());
            return list.Select(s => new EBillingDocumentDTO { Id = s.Id, Code = s.Code, Name = s.Name }).OrderBy(o => o.Code).ToList();
        }

        public async Task UpdateAsync(EBillingDocumentDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if(model is not null)
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
