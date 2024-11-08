using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class BranchBL : GenericBL<IBranchRep>, IBranchBL
    {
        public BranchBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await Repository.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> AnyAsync(string code)
        {
            return await Repository.AnyAsync(f => f.Code == code);
        }

        public async Task CreateAsync(BranchDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code && f.CompanyId == dto.CompanyId);
            if (model is null)
            {
                model = Mapper.Map<Branch>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"El código de la sucursal [{model.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<BranchDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(Guid.Parse(ids[1].ToString()));
            if (model is null)
                return new BranchDTO { CompanyId = Guid.Parse(ids[0].ToString()) };

            return Mapper.Map<BranchDTO>(model);
        }

        public Task<List<BranchDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BranchListDTO>> GetBranchListAsync(PaginationDTO pagination, string companyid)
        {
            var query = (from b in DbContext.Branch.AsNoTracking()
                         join bt in DbContext.BranchType on b.BranchTypeId equals bt.Id
                         where b.CompanyId == companyid.GetGuid()
                         select new BranchListDTO
                         {
                             Id = b.Id,
                             BranchType = bt.Name,
                             Code = b.Code,
                             Name = b.Name,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<BranchListDTO, BranchListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<BranchDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(BranchDTO dto)
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

        public async Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string branchid)
        {
            var query = (from add in DbContext.Address.AsNoTracking()
                         join addt in DbContext.AddressType.AsNoTracking() on add.AddressTypeId equals addt.Id
                         join addc in DbContext.Country.AsNoTracking() on add.CountryId equals addc.Id
                         join addr in DbContext.Region.AsNoTracking() on add.RegionId equals addr.Id
                         join addci in DbContext.City.AsNoTracking() on add.CityId equals addci.Id
                         where add.BranchId == branchid.GetGuid()
                         select new AddressDTO
                         {
                             Id = add.Id,
                             AddressType = addt.Name,
                             Address1 = add.Address1,
                             Country = addc.Name,
                             Region = addr.Name,
                             City = addci.Name
                         });
            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<AddressDTO, AddressDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<AddressDTO> GetAddressById(string branchid, string id)
        {
            var model = await DbContext.Address.AsNoTracking().Include(i => i.Emails)
                                                              .Include(i => i.Phones)
                                                              .FirstOrDefaultAsync(f => f.Id == id.GetGuid());
            if (model is null)
                return new AddressDTO { BranchId = branchid.GetGuid() };

            return Mapper.Map<AddressDTO>(model);
        }
    }
}
