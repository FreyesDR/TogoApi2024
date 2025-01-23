using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerCompanyEP
    {
        public static RouteGroupBuilder MapPartnerCompany(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapPost("/", Create);
            builder.MapDelete("/{partnerid}/{companyid}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<PartnerCompanyDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(string partnerid, IPartnerCompanyBL partnerCompanyBL)
        {
            return TypedResults.Ok(await partnerCompanyBL.GetListAsync(partnerid.GetGuid()));
        }
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string partnerid, string companyid, IPartnerCompanyBL partnerCompanyBL)
        {
            await partnerCompanyBL.DeleteAsync(partnerid.GetGuid(), companyid.GetGuid());
            return TypedResults.Ok();
        }
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(string partnerid, string companyid, IPartnerCompanyBL partnerCompanyBL)
        {
            try
            {
                await partnerCompanyBL.CreateAsync(new PartnerCompanyDTO { PartnerId = partnerid.GetGuid(), CompanyId = companyid.GetGuid() });
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }
    }
}
