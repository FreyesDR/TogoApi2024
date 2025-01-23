using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PointSaleEP
    {
        public static RouteGroupBuilder MapPointSale(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/{branchid}/list", GetList);
            builder.MapGet("/{branchid}/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PointSaleDTO>>(); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PointSaleDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<PointSaleDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string branchid, IPointSaleBL pointSaleBL)
        {
            return TypedResults.Ok(await pointSaleBL.GetPointSaleListAsync(pagination, branchid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPointSaleBL pointSaleBL)
        {
            await pointSaleBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PointSaleDTO dto, IPointSaleBL pointSaleBL)
        {
            try
            {
                await pointSaleBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PointSaleDTO dto, IPointSaleBL pointSaleBL)
        {
            try
            {
                await pointSaleBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PointSaleDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string branchid, string id, IPointSaleBL pointSaleBL)
        {
            return TypedResults.Ok(await pointSaleBL.GetByIdAsync(branchid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PointSaleDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string branchid, IPointSaleBL pointSaleBL)
        {
            return TypedResults.Ok(await pointSaleBL.GetListAsync(branchid));
        }
    }
}
