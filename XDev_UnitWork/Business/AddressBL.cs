using XDev_UnitWork.Interfaces;
using XDev_Model.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.Business
{
    public class AddressBL : GenericBL<IAddressRep>, IAddressBL
    {
        public AddressBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(AddressDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            if (model is null)
            {
                model = Mapper.Map<Address>(dto);
                if (model.CompanyId == Guid.Empty)
                    model.CompanyId = null;

                if (model.BranchId == Guid.Empty)
                    model.BranchId = null;

                if (model.PartnerId == Guid.Empty)
                    model.PartnerId = null;                

                model.Country = null;
                model.Region = null;
                model.City = null;
                model.Company = null;
                model.Branch = null;
                model.Partner = null;
                model.AddressType = null;

                await Repository.CreateAsync(model);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public Task<AddressDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<AddressDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<AddressDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(AddressDTO dto)
        {

            var model = await DbContext.Address.Include(i => i.Emails)
                                               .Include(i => i.Phones).FirstOrDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if (model is not null)
                {
                    model.Address1 = dto.Address1;
                    model.CountryId = dto.CountryId;
                    model.RegionId = dto.RegionId;
                    model.CityId = dto.CityId;
                    model.AddressTypeId = dto.AddressTypeId;

                    // Insert Emails
                    var news = dto.Emails.Where(w => !model.Emails.Any(f => w.Id == f.Id)).ToList();
                    await DbContext.AddressEmail.AddRangeAsync(Mapper.Map<List<AddressEmail>>(news));

                    // Delete Emails
                    var dels = model.Emails.Where(w => !dto.Emails.Any(f => w.Id == f.Id)).ToList();
                    DbContext.AddressEmail.RemoveRange(dels);

                    // Update Emails
                    foreach (var email in model.Emails)
                    {
                        var exists = dto.Emails.FirstOrDefault(f => f.Id == email.Id);
                        if (exists is not null)
                        {
                            email.Email = exists.Email;
                            email.Principal = exists.Principal;
                        }
                    }

                    //Console.WriteLine(DbContext.ChangeTracker.DebugView.LongView);
                    //DbContext.ChangeTracker.DetectChanges();
                    //Console.WriteLine(DbContext.ChangeTracker.DebugView.LongView);

                    // Insert Phones
                    var newsP = dto.Phones.Where(w => !model.Phones.Any(f => w.Id == f.Id)).ToList();
                    await DbContext.AddressPhone.AddRangeAsync(Mapper.Map<List<AddressPhone>>(newsP));

                    // Delete Phones
                    var delsP = model.Phones.Where(w => !dto.Phones.Any(f => w.Id == f.Id)).ToList();
                    DbContext.AddressPhone.RemoveRange(delsP);

                    // Update Phones
                    foreach (var phone in model.Phones)
                    {
                        var exists = dto.Phones.FirstOrDefault(f => f.Id == phone.Id);
                        if (exists is not null)
                        {
                            phone.Phone = exists.Phone;
                            phone.PhoneExt = exists.PhoneExt;
                        }
                    }

                    DbContext.Entry(model).Property(p => p.ConcurrencyStamp).OriginalValue = dto.ConcurrencyStamp;
                    await DbContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }

        public async Task<List<AddressDTO>> GetByPartnerId(Guid partnerid)
        {
            var address = DbContext.Address.AsNoTracking();            
            var addrtype = DbContext.AddressType.AsNoTracking();            
            var country = DbContext.Country.AsNoTracking();
            var region = DbContext.Region.AsNoTracking();
            var city = DbContext.City.AsNoTracking();

            var query = await (from addr in address
                               join addt in addrtype on addr.AddressTypeId equals addt.Id                               
                               join addco in country on addr.CountryId equals addco.Id
                               join addre in region on addr.RegionId equals addre.Id
                               join addci in city on addr.CityId equals addci.Id
                               
                               where addr.PartnerId == partnerid
                               select new AddressDTO
                               {
                                   Id = addr.Id,
                                   AddressTypeCode = addt.Code,
                                   Address1 = addr.Address1,
                                   Country = addco.Name,
                                   CountryCode = addco.Code,
                                   CountryAltCode = addco.CodeMH,
                                   Region = addre.Name,
                                   RegionCode = addre.Code,
                                   City = addci.Name,
                                   CityCode = addci.Code,                                   
                               }).ToListAsync();


            return query;            
        }

        public async Task<List<AddressDTO>> GetByBranchId(Guid branchid)
        {
            var address = DbContext.Address.AsNoTracking();            
            var addrtype = DbContext.AddressType.AsNoTracking();            
            var country = DbContext.Country.AsNoTracking();
            var region = DbContext.Region.AsNoTracking();
            var city = DbContext.City.AsNoTracking();

            var query = await (from addr in address
                               join addt in addrtype on addr.AddressTypeId equals addt.Id
                               join addco in country on addr.CountryId equals addco.Id
                               join addre in region on addr.RegionId equals addre.Id
                               join addci in city on addr.CityId equals addci.Id

                               where addr.BranchId == branchid
                               select new AddressDTO
                               {
                                   Id = addr.Id,
                                   AddressTypeCode = addt.Code,
                                   Address1 = addr.Address1,
                                   Country = addco.Name,
                                   CountryCode = addco.Code,
                                   CountryAltCode = addco.CodeMH,
                                   Region = addre.Name,
                                   RegionCode = addre.Code,
                                   City = addci.Name,
                                   CityCode = addci.Code
                               }).ToListAsync();


            return query;            
        }

        public async Task<List<AddressDTO>> GetByCompanyId(Guid companyid)
        {
            var address = DbContext.Address.AsNoTracking();            
            var addrtype = DbContext.AddressType.AsNoTracking();            
            var country = DbContext.Country.AsNoTracking();
            var region = DbContext.Region.AsNoTracking();
            var city = DbContext.City.AsNoTracking();

            var query = await (from addr in address
                               join addt in addrtype on addr.AddressTypeId equals addt.Id
                               join addco in country on addr.CountryId equals addco.Id
                               join addre in region on addr.RegionId equals addre.Id
                               join addci in city on addr.CityId equals addci.Id

                               where addr.CompanyId == companyid
                               select new AddressDTO
                               {
                                   Id = addr.Id,
                                   AddressTypeCode = addt.Code,
                                   Address1 = addr.Address1,
                                   Country = addco.Name,
                                   CountryCode = addco.Code,
                                   CountryAltCode = addco.CodeMH,
                                   Region = addre.Name,
                                   RegionCode = addre.Code,
                                   City = addci.Name,
                                   CityCode = addci.Code,                                   
                               }).ToListAsync();


            return query;
        }

        public async Task<List<AddressEmailDTO>> GetEmails(Guid addressid)
        {
            return await (from e in DbContext.AddressEmail.AsNoTracking()
                          where e.AddressId == addressid
                          select new AddressEmailDTO
                          {
                              Email = e.Email,
                              Principal = e.Principal,
                          }).ToListAsync();
        }

        public async Task<List<AddressPhoneDTO>> GetPhones(Guid addressid)
        {
            return await (from e in DbContext.AddressPhone.AsNoTracking()
                          where e.AddressId == addressid
                          select new AddressPhoneDTO
                          {
                              Phone = e.Phone,
                              PhoneExt = e.PhoneExt,
                          }).ToListAsync();
        }
    }
}
