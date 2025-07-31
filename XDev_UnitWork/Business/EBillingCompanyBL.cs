using AutoMapper;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using System.Text;
using XDev_AvaLinkAIO;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class EBillingCompanyBL : GenericBL<IEBillingCompanyRep>, IEBillingCompanyBL
    {
        private readonly IDataProtector _protectorCert;
        private readonly IDataProtector _protectorCred;
        private readonly IDataProtector _protectorSMTP;

        public EBillingCompanyBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper, IDataProtectionProvider dataProtectionProvider) : base(dbContext, contextAccessor, mapper)
        {
            _protectorCert = dataProtectionProvider.CreateProtector("cert-protector");
            _protectorCred = dataProtectionProvider.CreateProtector("cred-protector");
            _protectorSMTP = dataProtectionProvider.CreateProtector("smtp");
        }

        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(EBillingCompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId);
            if (model is null)
            {
                model = Mapper.Map<EBillingCompany>(dto);
                model.EBilling = null;

                if (!dto.ApiKeyTest.IsNullOrEmpty())
                    model.ApiKeyTest = _protectorCred.Protect(dto.ApiKeyTest);
                if (!dto.ApiKeyProd.IsNullOrEmpty())
                    model.ApiKeyProd = _protectorCred.Protect(dto.ApiKeyProd);
                if (!dto.PrivateKeyTest.IsNullOrEmpty())
                    model.PrivateKeyTest = _protectorCred.Protect(dto.PrivateKeyTest);
                if (!dto.PrivateKeyProd.IsNullOrEmpty())
                    model.PrivateKeyProd = _protectorCred.Protect(dto.PrivateKeyProd);

                model.SmtpUserPassword = _protectorSMTP.Protect(dto.SmtpUserPassword);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Sociedad ya está registrada para Facturación Electrónica");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<EBillingCompanyDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new EBillingCompanyDTO { EBillingId = ids[0].ToString().GetGuid() };

            model.SmtpUserPassword = string.Empty;
            model.ApiKeyTest = string.Empty;
            model.ApiKeyProd = string.Empty;
            model.PrivateKeyTest = string.Empty;
            model.PrivateKeyProd = string.Empty;

            return Mapper.Map<EBillingCompanyDTO>(model);
        }

        public Task<List<EBillingCompanyDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EBillingCompanyListDTO>> GetListAsync(PaginationDTO pagination, string ebillingid)
        {
            var query = (from ebc in DbContext.EBillingCompany.AsNoTracking()
                         join co in DbContext.Company.AsNoTracking() on ebc.CompanyId equals co.Id
                         where ebc.EBillingId == ebillingid.GetGuid()
                         select new EBillingCompanyListDTO
                         {
                             EBillingId = ebc.EBillingId,
                             CompanyId = ebc.CompanyId,
                             CompanyCode = co.Code,
                             CompanyName = co.Name,
                             IsProd = ebc.IsProd,
                             Active = ebc.Active,
                             Contingency = ebc.Contingency,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<EBillingCompanyListDTO, EBillingCompanyListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<EBillingCompanyDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(EBillingCompanyDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.EBillingId == dto.EBillingId && f.CompanyId == dto.CompanyId);
            try
            {
                if (model is not null)
                {
                    model.ApiUser = dto.ApiUser;
                    if (!dto.ApiKeyTest.IsNullOrEmpty())
                        model.ApiKeyTest = _protectorCred.Protect(dto.ApiKeyTest);
                    if (!dto.ApiKeyProd.IsNullOrEmpty())
                        model.ApiKeyProd = _protectorCred.Protect(dto.ApiKeyProd);
                    if (!dto.PrivateKeyTest.IsNullOrEmpty())
                        model.PrivateKeyTest = _protectorCred.Protect(dto.PrivateKeyTest);
                    if (!dto.PrivateKeyProd.IsNullOrEmpty())
                        model.PrivateKeyProd = _protectorCred.Protect(dto.PrivateKeyProd);

                    model.IsProd = dto.IsProd;
                    model.Active = dto.Active;
                    model.Contingency = dto.Contingency;
                    model.SmtpService = dto.SmtpService;
                    model.SmtpHost = dto.SmtpHost;
                    model.SmtpPort = dto.SmtpPort;
                    model.SmtpUserName = dto.SmtpUserName;
                    model.CcEmail1 = dto.CcEmail1;
                    model.CcEmail2 = dto.CcEmail2;

                    if (dto.SmtpUserPassword.IsNotNullOrEmpty())
                        model.SmtpUserPassword = XDev_AvaLinkAIO.AIO.Encrypt(dto.SmtpUserPassword);

                    model.SmtpEnableSsl = dto.SmtpEnableSsl;
                    model.FromName = dto.FromName;

                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }

        public async Task UploadCertificados(EBillinCertsDTO dto)
        {
            if (dto.FileTest is null && dto.FilePrd is null)
                return;

            var model = await Repository.GetFirstorDefaultAsync(f => f.CompanyId == dto.CompanyId.GetGuid());
            if (model is not null)
            {
                if (dto.FileTest is not null)
                {
                    using var reader = new StreamReader(dto.FileTest.OpenReadStream());
                    var content = await reader.ReadToEndAsync();

                    if (!content.Contains("<CertificadoMH>"))
                        throw new CustomTogoException("El certificado no tiene la estructura esperada");

                    var encrypted = _protectorCert.Protect(content);
                    model.CertTest = Encoding.UTF8.GetBytes(encrypted);

                }

                if (dto.FilePrd is not null)
                {
                    using var reader = new StreamReader(dto.FilePrd.OpenReadStream());
                    var content = await reader.ReadToEndAsync();

                    if (!content.Contains("<CertificadoMH>"))
                        throw new CustomTogoException("El certificado no tiene la estructura esperada");

                    var encrypted = _protectorCert.Protect(content);
                    model.CertPrd = Encoding.UTF8.GetBytes(encrypted);

                }

                await Repository.UpdateAsync(model);
            }
        }
    }
}
