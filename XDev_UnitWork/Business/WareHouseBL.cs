using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Business
{
    public class WareHouseBL : GenericBL<IWareHouseRep>, IWareHouseBL
    {
        public WareHouseBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(WareHouseDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code && f.BranchId == dto.BranchId);
            if (model is null)
            {
                model = Mapper.Map<WareHouse>(dto);
                model.Branch = null;

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"El código de almacén [{model.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<WareHouseDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new WareHouseDTO { BranchId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<WareHouseDTO>(model);
        }

        public Task<List<WareHouseDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<WareHouseDTO>> GetWareHouseListAsync(PaginationDTO pagination, string branchid)
        {
            var query = await Repository.QueryAsync(f => f.BranchId == branchid.GetGuid());
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<WareHouse, WareHouseDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<WareHouseDTO>> GetListAsync()
        {
            var list = await Repository.GetListAsync();
            return list.Select(x => new WareHouseDTO 
            { 
                Id = x.Id, 
                BranchId = x.BranchId ,
                Code = x.Code,
                Name = x.Name              
            }).OrderBy(o => o.Code).ToList();
        }

        public async Task<List<WareHouseDTO>> GetListAsync(string branchid)
        {
            var list = await Repository.GetListAsync(f => f.BranchId == branchid.GetGuid());
            return list.Select(x => new WareHouseDTO
            {
                Id = x.Id,
                BranchId = x.BranchId,
                Code = x.Code,
                Name = x.Name
            }).OrderBy(o => o.Code).ToList();
        }

        public async Task UpdateAsync(WareHouseDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if (model is not null)
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
