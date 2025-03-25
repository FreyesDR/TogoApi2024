using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Business
{
    public class PartnerIDBL : GenericBL<IPartnerIDRep>, IPartnerIDBL
    {
        public PartnerIDBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(PartnerIDDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.PartnerId == dto.PartnerId && f.IDTypeId == dto.IDTypeId);
            if (model is null)
            {
                model = Mapper.Map<PartnerID>(dto);
                model.Partner = null;
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

        public async Task<PartnerIDDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new PartnerIDDTO { PartnerId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<PartnerIDDTO>(model);
        }

        public Task<List<PartnerIDDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerIDDTO>> GetListAsync(PaginationDTO pagination, string partnerid)
        {
            var query = (from pi in DbContext.PartnerID.AsNoTracking()
                         join it in DbContext.IDType.AsNoTracking() on pi.IDTypeId equals it.Id
                         where pi.PartnerId == partnerid.GetGuid()
                         select new PartnerIDDTO
                         {
                             Id = pi.Id,
                             IDType = it.Name,
                             DocumentNumber = pi.DocumentNumber,
                             DateIssue = pi.DateIssue,
                             DateExpira = pi.DateExpira,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PartnerIDDTO, PartnerIDDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<PartnerIDDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PartnerIDDTO dto)
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
