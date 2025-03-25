using AutoMapper;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class EBillingTaxBL : GenericBL<IEBillingTaxRep>, IEBillingTaxBL
    {
        public EBillingTaxBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public Task CreateAsync(EBillingTaxDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EBillingTaxDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<EBillingTaxDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingTaxDTO>> GetListAsync(PaginationDTO pagination, string ebillingid)
        {
            var query = await Repository.QueryAsync(f => f.EBillingId == ebillingid.GetGuid());
            query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<EBillingTax, EBillingTaxDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<EBillingTaxDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(EBillingTaxDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
