
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using NPOI.SS.Formula.Functions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingDocumentEP
    {
        public static RouteGroupBuilder MapEBillingDocument(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination);
            builder.MapGet("/{ebillingid}/{id}", GetById);
            builder.MapGet("/{ebillingid}/list", GetList);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<EBillingDocumentDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EBillingDocumentDTO>>();
            return builder;
        }

        private static async Task<Results<Ok<List<EBillingDocumentDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string ebillingid, IEBillingDocumentBL eBillingDocumentBL)
        {
            return TypedResults.Ok(await eBillingDocumentBL.GetListAsync(ebillingid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EBillingDocumentDTO dto, IEBillingDocumentBL eBillingDocumentBL)
        {
            try
            {
                await eBillingDocumentBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(EBillingDocumentDTO dto, IEBillingDocumentBL eBillingDocumentBL)
        {
            try
            {
                await eBillingDocumentBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<EBillingDocumentDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, string ebillingid, IEBillingDocumentBL eBillingDocumentBL)
        {
            return TypedResults.Ok(await eBillingDocumentBL.GetByIdAsync(ebillingid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<EBillingDocumentDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, string ebillingid, IEBillingDocumentBL eBillingDocumentBL)
        {
            return TypedResults.Ok(await eBillingDocumentBL.GetListAsync(dto, ebillingid));
        }
    }
}
