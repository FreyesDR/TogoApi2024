using Microsoft.AspNetCore.Http.HttpResults;
using NPOI.Util;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class SaleOrderTypeEP
    {
        public static RouteGroupBuilder MapSaleOrderType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", Get);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<SaleOrderTypeDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<SaleOrderTypeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<SaleOrderTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(ISaleOrderTypeBL saleOrderTypeBL)
        {
            return TypedResults.Ok(await saleOrderTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ISaleOrderTypeBL saleOrderType)
        {
            await saleOrderType.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(SaleOrderTypeDTO dto, ISaleOrderTypeBL saleOrderType)
        {
            try
            {
                await saleOrderType.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(SaleOrderTypeDTO dto, ISaleOrderTypeBL saleOrderType)
        {
            try
            {
                await saleOrderType.CreateAsync(dto);
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

        private static async Task<Results<Ok<SaleOrderTypeDTO>, BadRequest>> Get(string id, ISaleOrderTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetByIdAsync(id == "0" ? Guid.Empty : id.GetGuid()));
        }

        private static async Task<Results<Ok<List<SaleOrderTypeDTO>>, BadRequest>> GetAll(PaginationDTO dto, ISaleOrderTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync(dto));
        }
    }
}
