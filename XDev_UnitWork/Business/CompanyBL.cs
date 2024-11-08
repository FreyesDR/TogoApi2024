using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class CompanyBL : GenericBL<ICompanyRep>, ICompanyBL
    {
        public CompanyBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(CompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                model = Mapper.Map<Company>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"El código [{dto.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<CompanyDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new CompanyDTO();

            return Mapper.Map<CompanyDTO>(model);
        }

        public async Task<CompanyDTO> GetByCode(string code)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == code);
            if (model is null)
                return null;

            return Mapper.Map<CompanyDTO>(model);
        }

        public Task<List<CompanyDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CompanyListDTO>> GetCompanyListAsync(PaginationDTO pagination)
        {
            var query = (from co in DbContext.Company.AsNoTracking()
                         join ct in DbContext.CompanyType.AsNoTracking() on co.CompanyTypeId equals ct.Id
                         select new CompanyListDTO
                         {
                             Id = co.Id,
                             Code = co.Code,
                             Name = co.Name,
                             TradeName = co.TradeName,
                             CompanyType = ct.Name,
                             Active = co.Active,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<CompanyListDTO, CompanyListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<CompanyListDTO>> GetCompanyListAsync()
        {
            var list = await Repository.GetListAsync(f => f.Active == true);
            return Mapper.Map<List<CompanyListDTO>>(list.Select(s => new CompanyListDTO
            {
                Id = s.Id,
                Name = s.Name,
                TradeName = s.TradeName,
                Code = s.Code
            }).OrderBy(o => o.Code).ToList());
        }

        public Task<List<CompanyDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(CompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if (model is not null)
                {
                    model.CompanyTypeId = dto.CompanyTypeId;
                    model.Name = dto.Name;
                    model.TradeName = dto.TradeName;
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

        public async Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string companyid)
        {
            var query = (from add in DbContext.Address.AsNoTracking()
                         join addt in DbContext.AddressType.AsNoTracking() on add.AddressTypeId equals addt.Id
                         join addc in DbContext.Country.AsNoTracking() on add.CountryId equals addc.Id
                         join addr in DbContext.Region.AsNoTracking() on add.RegionId equals addr.Id
                         join addci in DbContext.City.AsNoTracking() on add.CityId equals addci.Id
                         where add.CompanyId == companyid.GetGuid()
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

        public async Task<AddressDTO> GetAddressById(string companyid, string id)
        {
            var model = await DbContext.Address.AsNoTracking().Include(i => i.Emails)
                                                              .Include(i => i.Phones)
                                                              .FirstOrDefaultAsync(f => f.Id == id.GetGuid());
            if (model is null)
                return new AddressDTO { CompanyId = companyid.GetGuid() };

            return Mapper.Map<AddressDTO>(model);
        }
    }
}
