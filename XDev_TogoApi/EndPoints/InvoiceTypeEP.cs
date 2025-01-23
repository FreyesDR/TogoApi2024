
using Microsoft.AspNetCore.Http.HttpResults;
using NPOI.SS.Formula.Functions;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XDev_TogoApi.EndPoints
{
    public static class InvoiceTypeEP
    {
        public static RouteGroupBuilder MapInvoiceType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", Get);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<InvoiceTypeDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<InvoiceTypeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<InvoiceTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IInvoiceTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IInvoiceTypeBL invoiceTypeBL)
        {
            await invoiceTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(InvoiceTypeDTO dto, IInvoiceTypeBL invoiceTypeBL)
        {
            try
            {
                await invoiceTypeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(InvoiceTypeDTO dto, IInvoiceTypeBL invoiceTypeBL)
        {
            try
            {
                await invoiceTypeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<InvoiceTypeDTO>, BadRequest>> Get(string id, IInvoiceTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetByIdAsync(id == "0" ? Guid.Empty:id.GetGuid() ));
        }

        private static async Task<Results<Ok<List<InvoiceTypeDTO>>, BadRequest>> GetAll(PaginationDTO dto, IInvoiceTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync(dto));
        }
    }
}
