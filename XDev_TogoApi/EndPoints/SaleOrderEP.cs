
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class SaleOrderEP
    {
        public static RouteGroupBuilder MapSaleOrder(this RouteGroupBuilder builder) 
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/{id}", GetById);
            builder.MapGet("/pdf/{id}", GetPdfForm);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<SaleOrderDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<SaleOrderDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<FileStreamHttpResult,BadRequest<ExceptionReturnDTO>>> GetPdfForm(string id, ISaleOrderBL saleOrderBL)
        {
            try
            {
                var pdf = await saleOrderBL.GetPdfFormAsync(id.GetGuid());
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ISaleOrderBL saleOrderBL)
        {
            try
            {
                await saleOrderBL.DeleteAsync(id.GetGuid());
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(SaleOrderDTO dto, ISaleOrderBL saleOrderBL)
        {
            try
            {
                await saleOrderBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(SaleOrderDTO dto, ISaleOrderBL saleOrderBL)
        {
            try
            {
                await saleOrderBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<SaleOrderDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, ISaleOrderBL saleOrderBL)
        {
            return TypedResults.Ok(await saleOrderBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<SaleOrderListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO dto, ISaleOrderBL saleOrder)
        {
            return TypedResults.Ok(await saleOrder.GetSOListAsync(dto));
        }
    }
}
