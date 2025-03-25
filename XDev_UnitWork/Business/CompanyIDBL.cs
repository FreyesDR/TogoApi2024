using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Business
{
    public class CompanyIDBL : GenericBL<ICompanyIDRep>, ICompanyIDBL
    {
        public CompanyIDBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(CompanyIDDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId && f.IDTypeId == dto.IDTypeId);
            if (model is null)
            {
                model = Mapper.Map<CompanyID>(dto);
                model.Company = null;
                model.IDType = null;

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Tipo identificación ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<CompanyIDDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new CompanyIDDTO { CompanyId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<CompanyIDDTO>(model);
        }

        public Task<List<CompanyIDDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CompanyIDDTO>> GetListAsync(PaginationDTO pagination, string companyid)
        {
            var query = (from ci in DbContext.CompanyID.AsNoTracking()
                         join it in DbContext.IDType.AsNoTracking() on ci.IDTypeId equals it.Id
                         where ci.CompanyId == companyid.GetGuid()
                         select new CompanyIDDTO
                         {
                             Id = ci.Id,                             
                             IDType = it.Name,
                             DocumentNumber = ci.DocumentNumber,
                             DateIssue = ci.DateIssue,
                             DateExpira = ci.DateExpira,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<CompanyIDDTO, CompanyIDDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<CompanyIDDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CompanyIDDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.Id);
            try
            {
                if (model is not null)
                {
                    model.DocumentNumber = dto.DocumentNumber;
                    model.IDTypeId = dto.IDTypeId;
                    model.DateIssue = dto.DateIssue;
                    model.DateExpira = dto.DateExpira;
                    model.NIFNum = dto.NIFNum;

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
