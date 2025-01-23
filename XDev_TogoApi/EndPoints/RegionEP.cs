using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class RegionEP
    {
        public static RouteGroupBuilder MapRegion(this RouteGroupBuilder builder)
        {
            builder.MapGet("/all", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<RegionDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<RegionDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IRegionBL regionBL)
        {
            await regionBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(RegionDTO dto, IRegionBL regionBL)
        {
            try
            {
                await regionBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(RegionDTO dto, IRegionBL regionBL)
        {
            try
            {
                await regionBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<RegionDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string countryid, string id, IRegionBL regionBL)
        {
            return TypedResults.Ok(await regionBL.GetByIdAsync(id.GetGuid(), countryid.GetGuid()));
        }

        private static async Task<Results<Ok<List<RegionDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string countryid, IRegionBL regionBL)
        {
            return TypedResults.Ok(await regionBL.GetListAsync(countryid.GetGuid()));
        }

        private static async Task<Results<Ok<List<RegionDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string countryid, IRegionBL regionBL)
        {
            return TypedResults.Ok(await regionBL.GetListAsync(pagination, countryid.GetGuid()));
        }
    }
}
