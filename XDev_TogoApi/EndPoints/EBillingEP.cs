
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingEP
    {
        public static RouteGroupBuilder MapEBilling(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination);
            builder.MapGet("/{id}", GetByID);
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EBillingDTO>>();
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EBillingDTO dto, IEBillingBL eBillingBL)
        {
            try
            {
                await eBillingBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok<EBillingDTO>, BadRequest<ExceptionReturnDTO>>> GetByID(string id, IEBillingBL eBillingBL)
        {
            return TypedResults.Ok(await eBillingBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<EBillingDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, IEBillingBL eBillingBL)
        {
            return TypedResults.Ok(await eBillingBL.GetListAsync(dto));
        }
    }
}
