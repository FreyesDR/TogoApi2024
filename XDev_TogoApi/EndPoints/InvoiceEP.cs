
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.FeSv;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class InvoiceEP
    {
        public static RouteGroupBuilder MapInvoice(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Factura"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Factura"));
            builder.MapGet("/pdf/{id}", GetPdfForm).WithDescription("Obtener PDF").WithMetadata(new ModuleAttribute("Factura"));
            builder.MapGet("/cancel/{id}", GetInfoCancel).WithDescription("Obtener Información para Anular").WithMetadata(new ModuleAttribute("Factura"));
            builder.MapPost("/cancel", Cancel).WithDescription("Anular").WithMetadata(new ModuleAttribute("Factura"));
            return builder;
        }

        private static async Task<Results<Ok<FeCancelDTO>, BadRequest<ExceptionReturnDTO>>> GetInfoCancel(string id, IInvoiceBL invoiceBL)
        {
            try
            {
                return TypedResults.Ok(await invoiceBL.GetInvoiceCancelAsync(id));
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        private static async Task<Results<Ok<FeResponseDTO>, BadRequest<FeResponseDTO>>> Cancel(FeCancelDTO dto, IInvoiceBL invoiceBL)
        {
            try
            {
                return TypedResults.Ok(await invoiceBL.CancelInvoice(dto));
            }
            catch (Exception ex)
            {
                return TypedResults.BadRequest(new FeResponseDTO
                {
                    StatusCode = StatusCodes.Status400BadRequest.ToString(),
                    Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
            
        }

        private static async Task<Results<Ok<InvoiceDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IInvoiceBL invoiceBL)
        {
            try
            {
                return TypedResults.Ok(await invoiceBL.GetByIdAsync(id.GetGuid()));
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

        private static async Task<Results<Ok<List<InvoiceListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO dto, IInvoiceBL invoiceBL)
        {
            return TypedResults.Ok(await invoiceBL.GetPaginationAsync(dto));
        }

        private static async Task<Results<FileStreamHttpResult, BadRequest<ExceptionReturnDTO>>> GetPdfForm(string id, IInvoiceBL invoiceBL)
        {
            try
            {
                var pdf = await invoiceBL.CreateFormPDF(id.GetGuid());
                return TypedResults.File(pdf, "application/pdf", $"{Guid.Parse(id).ToString("N")}.pdf");
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
    }
}
