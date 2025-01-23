using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class NumberRangeEP
    {
        public static RouteGroupBuilder MapNumberRange(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<NumberRangeDTO>>(); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<NumberRangeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, INumberRangeBL numberRangeBL)
        {
            await numberRangeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(NumberRangeDTO dto, INumberRangeBL numberRangeBL)
        {
            try
            {
                await numberRangeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(NumberRangeDTO dto, INumberRangeBL numberRangeBL)
        {
            try
            {
                await numberRangeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<NumberRangeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, INumberRangeBL numberRangeBL)
        {
            return TypedResults.Ok(await numberRangeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<NumberRangeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(INumberRangeBL numberRangeBL)
        {
            return TypedResults.Ok(await numberRangeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<NumberRangeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, INumberRangeBL numberRangeBL)
        {
            return TypedResults.Ok(await numberRangeBL.GetListAsync(pagination));
        }
    }
}
