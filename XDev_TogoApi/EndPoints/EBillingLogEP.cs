using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingLogEP
    {
        public static RouteGroupBuilder MapEBillingLog(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Facturación Electrónica Log"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Facturación Electrónica Log"));
            builder.MapGet("/invoice/{invoiceid}", GetByInvoice).WithDescription("Obtener por Id de Factura").WithMetadata(new ModuleAttribute("Facturación Electrónica Log"));
            return builder;
        }

        private static async Task<Results<Ok<EBillingLogDTO>, BadRequest<ExceptionReturnDTO>>> GetByInvoice(string invoiceid, IEBillingLogBL eBillingLogBL)
        {
            try
            {
                return TypedResults.Ok(await eBillingLogBL.GetLogByInvoiceId(invoiceid.GetGuid()));

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

        private static async Task<Results<Ok<EBillingLogDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IEBillingLogBL eBillingLogBL)
        {
            try
            {
                return TypedResults.Ok(await eBillingLogBL.GetLogByIdAsync(id.GetGuid()));
                
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

        private static async Task<Results<Ok<List<EBillingLogDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, IEBillingLogBL eBillingBL)
        {
            return TypedResults.Ok(await eBillingBL.GetPaginationAsync(dto));
        }
    }
}
