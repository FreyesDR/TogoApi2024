using NPOI.SS.Formula.Functions;
using NPOI.Util;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.SaleOrder;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.PriceScheme;

namespace XDev_TogoApi.EndPoints
{
    public static class PriceConditionEP
    {
        public static RouteGroupBuilder MapPriceCondition(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", Get);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PriceConditionDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PriceConditionDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<PriceConditionDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPriceConditionBL priceConditionBL)
        {
            return TypedResults.Ok(await priceConditionBL.GetListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPriceConditionBL saleOrderType)
        {
            await saleOrderType.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PriceConditionDTO dto, IPriceConditionBL saleOrderType)
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PriceConditionDTO dto, IPriceConditionBL saleOrderType)
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

        private static async Task<Results<Ok<PriceConditionDTO>, BadRequest>> Get(string id, IPriceConditionBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetByIdAsync(id == "0" ? Guid.Empty : id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PriceConditionDTO>>, BadRequest>> GetAll(PaginationDTO dto, IPriceConditionBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync(dto));
        }
    }
}
