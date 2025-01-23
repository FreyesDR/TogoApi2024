using NPOI.Util;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using XDev_TogoApi.Code;
using XDev_UnitWork.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.DM;

namespace XDev_TogoApi.EndPoints
{
    public static class CurrencyEP
    {
        public static RouteGroupBuilder MapCurrency(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CurrencyDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CurrencyDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICurrencyBL currencyBL)
        {
            await currencyBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CurrencyDTO dto, ICurrencyBL currencyBL)
        {
            try
            {
                await currencyBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CurrencyDTO dto, ICurrencyBL currencyBL)
        {
            try
            {
                await currencyBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<CurrencyDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, ICurrencyBL currencyBL)
        {
            return TypedResults.Ok(await currencyBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CurrencyDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(ICurrencyBL currencyBL)
        {
            return TypedResults.Ok(await currencyBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<CurrencyDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, ICurrencyBL currencyBL)
        {
            return TypedResults.Ok(await currencyBL.GetListAsync(pagination));
        }
    }
}
