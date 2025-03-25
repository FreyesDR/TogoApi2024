
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class InvoiceTypeEP
    {
        public static RouteGroupBuilder MapInvoiceType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Tipo Factura"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Tipo Factura"));
            builder.MapGet("/{id}", Get).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Tipo Factura"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<InvoiceTypeDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Tipo Factura"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<InvoiceTypeDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Tipo Factura"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Tipo Factura"));
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
