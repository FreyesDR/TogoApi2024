using XDev_UnitWork.Interfaces;
using XDev_Model.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model.Entities;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Custom;

namespace XDev_UnitWork.Business
{
    public class AddressBL: GenericBL<IAddressRep>, IAddressBL
    {
        public AddressBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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
    }
}
