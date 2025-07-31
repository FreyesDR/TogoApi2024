using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class PartnerBL : GenericBL<IPartnerRep>, IPartnerBL
    {
        private readonly IAddressBL addressBL;

        public PartnerBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper, IAddressBL addressBL) : base(dbContext, contextAccessor, mapper)
        {
            this.addressBL = addressBL;
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await Repository.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> AnyAsync(string code)
        {
            return await Repository.AnyAsync(f => f.Code == code);
        }

        public async Task CreateAsync(PartnerDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                var partnercfg = await DbContext.PartnerFeatures.FirstOrDefaultAsync();
                if (partnercfg is null)
                    throw new CustomTogoException("Falta la personalización de socios");

                model = Mapper.Map<Partner>(dto);
                model.PartnerType = null;
                model.PaymentCondition = null;

                if (partnercfg.NumType == 1)
                {
                    
                    var result = DbContext.Database.SqlQuery<long>($"select * from xsp_gen_next_number({partnercfg.RangeId}::uuid)").ToList();
                    if (result.Count == 0)
                        throw new CustomTogoException("Error generando rango de número, validar configuración");

                    model.Code = result[0].ToString();
                }

                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException($"Código de socio '{model.Code}' ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetByIdAsync(id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<PartnerDTO> GetByIdAsync(params object[] ids)
        {
            var cfg = await DbContext.PartnerFeatures.FirstOrDefaultAsync();

            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new PartnerDTO { NumType = cfg.NumType.ToString() };

            var dto = Mapper.Map<PartnerDTO>(model);

            dto.NumType = cfg.NumType.ToString();
            return dto;
        }

        public async Task<PartnerDTO> GetByCodeAsync(string code, string companyid)
        {
            var patype = DbContext.PartnerRole.AsNoTracking().FirstOrDefault(f => f.Code == "D");

            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == code);
            if (model is null)
                throw new CustomTogoException($"No existe socio con el código [{code}]");

            var allow = await DbContext.PartnerCompany.AsNoTracking().FirstOrDefaultAsync(f => f.CompanyId == companyid.GetGuid());
            if (allow is null)
                throw new CustomTogoException($"Socio {code} no ampliado para sociedad seleccionada");

            var rol = await DbContext.PartnerRoles.AsNoTracking().FirstOrDefaultAsync(f => f.PartnerId == model.Id && f.RoleId == patype.Id);
            if (rol is null)
                throw new CustomTogoException($"Socio {code} no posee rol de cliente");

            return Mapper.Map<PartnerDTO>(model);
        }

        public Task<List<PartnerDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PartnerListDTO>> GetPartnerListAsync(PaginationDTO pagination)
        {
            var query = (from p in DbContext.Partner.AsNoTracking()
                         join pt in DbContext.PartnerType.AsNoTracking() on p.PartnerTypeId equals pt.Id
                         select new PartnerListDTO
                         {
                             Id = p.Id,
                             Code = p.Code,
                             OldCode = p.OldCode,
                             Name = p.Name,
                             TradeName = p.TradeName,
                             Active = p.Active,
                             PartnerType = pt.Name,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PartnerListDTO, PartnerListDTO>(pagination, ContextAccessor.HttpContext);
        }



        public Task<List<PartnerDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(PartnerDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if (model is not null)
                {
                    model.PartnerTypeId = dto.PartnerTypeId;
                    model.Name = dto.Name;
                    model.TradeName = dto.TradeName;
                    model.PaymentConditionId = dto.PaymentConditionId;
                    model.Active = dto.Active;
                    model.ContactPersonPhone = dto.ContactPersonPhone;
                    model.ContactPersonEmail = dto.ContactPersonEmail;
                    model.ContactPersonIDNumber = dto.ContactPersonIDNumber;
                    model.ContactPersonName = dto.ContactPersonName;

                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }

        public async Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string partnerid)
        {
            var query = (from add in DbContext.Address.AsNoTracking()
                         join addt in DbContext.AddressType.AsNoTracking() on add.AddressTypeId equals addt.Id
                         join addc in DbContext.Country.AsNoTracking() on add.CountryId equals addc.Id
                         join addr in DbContext.Region.AsNoTracking() on add.RegionId equals addr.Id
                         join addci in DbContext.City.AsNoTracking() on add.CityId equals addci.Id
                         where add.PartnerId == partnerid.GetGuid()
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

        public async Task<AddressDTO> GetAddressById(string partnerid, string id)
        {
            var model = await DbContext.Address.AsNoTracking().Include(i => i.Emails)
                                                              .Include(i => i.Phones)
                                                              .FirstOrDefaultAsync(f => f.Id == id.GetGuid());
            if (model is null)
                return new AddressDTO { PartnerId = partnerid.GetGuid() };

            return Mapper.Map<AddressDTO>(model);
        }

        public async Task<PartnerInfoDTO> GetInfoAsync(Guid partnerid)
        {
            //var addrType = await DbContext.AddressType.AsNoTracking().FirstOrDefaultAsync(f => f.Code == "DO");

            //if (addrType is null)
            //    throw new Exception("El tipo dirección 'DO' Domicilio no existe");

            var partner = await DbContext.Partner.Include(i => i.PartnerType)                                                     
                                                 .Include(i => i.EconomicActivities)
                                                    .ThenInclude(t => t.EconomicActivity).AsSplitQuery()                                                     
                                                 .AsNoTracking().FirstOrDefaultAsync(f => f.Id == partnerid);

            var dto = new PartnerInfoDTO()
            {
                Code = partner.Code,
                Name = partner.Name,
                TradeName = partner.TradeName,
                ContactPersonEmail = partner.ContactPersonEmail,
                ContactPersonIDNumber = partner.ContactPersonIDNumber,
                ContactPersonName = partner.ContactPersonName,
                ContactPersonPhone = partner.ContactPersonPhone,
                TypePerson = partner.PartnerType.Code == "P" ? "1" : "2"
            };

            var partnerIds = await DbContext.PartnerID.Include(i => i.IDType).Where(w => w.PartnerId == partnerid).ToListAsync();

            if (partnerIds is not null)
            {
                var nif = partnerIds.FirstOrDefault(f => f.NIFNum == "1");
                if(nif is null)
                    nif = partnerIds.FirstOrDefault();

                if(nif is not null)
                {
                    dto.Nif1Id = nif.IDTypeId;
                    dto.Nif1 = nif.DocumentNumber;
                    dto.Nif1Code = nif.IDType.AltCode;
                }

                nif = partnerIds.FirstOrDefault(f => f.NIFNum == "2");
                if(nif is not null)
                {
                    dto.Nif2Id = nif.IDTypeId;
                    dto.Nif2 = nif.DocumentNumber;
                    dto.Nif2Code = nif.IDType.AltCode;
                }

            }

            var addresses = await addressBL.GetByPartnerId(partnerid);
            
            if (addresses is not null)
            {
                var address = addresses.FirstOrDefault(f => f.AddressTypeCode == "DO");
                if (address is null)
                    address = addresses.FirstOrDefault();

                if (address is not null)
                {
                    dto.Address = $"{address.Address1}, {address.City}, {address.Region}, {address.Country}";

                    dto.CountryCode = address.CountryCode;
                    dto.CountryName = address.Country;
                    dto.CountryAltCode = address.CountryAltCode;
                    dto.RegionCode = address.RegionCode;
                    dto.RegionName = address.Region;
                    dto.CityCode = address.CityCode;
                    dto.CityName = address.City;

                    var emails = await addressBL.GetEmails(address.Id);
                    if (emails.Any())
                    {
                        var email = emails.FirstOrDefault(f => f.Principal == true);
                        if (email is null)
                            email = emails.FirstOrDefault();

                        if (email is not null)
                            dto.Email = email.Email;
                    }

                    var phones = await addressBL.GetPhones(address.Id);
                    if (phones.Any())
                    {
                        var tel = phones.FirstOrDefault();
                        if (tel is not null)
                            dto.Phone = tel.Phone;
                    }
                }
            }

            if (partner.EconomicActivities.Any())
            {
                var act = partner.EconomicActivities.FirstOrDefault(f => f.Principal == true);
                if(act is null)
                    act = partner.EconomicActivities.FirstOrDefault();

                if (act is not null)
                {
                    dto.EcoActCode = act.EconomicActivity.Code;
                    dto.EcoActName = act.EconomicActivity.Name;
                }
                    
            }

            return dto;
        }
    }
}
