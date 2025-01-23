
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Business;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_TogoApi.Code;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingCompanyEP
    {
        public static RouteGroupBuilder MapEBillingCompany(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination);
            builder.MapGet("/{ebillingid}/{companyid}", GetById);
            builder.MapGet("/address", GetListCompanyAddress);
            builder.MapGet("/idstype", GetListCompanyIDs);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<EBillingCompanyDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EBillingCompanyDTO>>();
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EBillingCompanyDTO dto, IEBillingCompanyBL eBillingCompanyBL)
        {
            try
            {
                await eBillingCompanyBL.UpdateAsync(dto);
                return TypedResults.Ok();
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.Message
                });
            }
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(EBillingCompanyDTO dto,IEBillingCompanyBL eBillingCompanyBL)
        {
            try
            {
                await eBillingCompanyBL.CreateAsync(dto);
                return TypedResults.Ok();
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.Message
                });
            }
        }

        private static async Task<Results<Ok<List<EBillingCompanyIDsDTO>>, BadRequest<ExceptionReturnDTO>>> GetListCompanyIDs(string companyid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetCompanyDocumentsIDs(companyid));
        }

        private static async Task<Results<Ok<List<EBillingCompanyAddressDTO>>, BadRequest<ExceptionReturnDTO>>> GetListCompanyAddress(string companyid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetCompanyAddress(companyid));
        }

        private static async Task<Results<Ok<EBillingCompanyDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string companyid, string ebillingid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetByIdAsync(ebillingid.GetGuid(), companyid.GetGuid()));
        }

        private static async Task<Results<Ok<List<EBillingCompanyListDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, string ebillingid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetListAsync(dto,ebillingid));
        }
    }
}
