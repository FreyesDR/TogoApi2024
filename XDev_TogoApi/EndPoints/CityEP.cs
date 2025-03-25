using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class CityEP
    {
        public static RouteGroupBuilder MapCity(this RouteGroupBuilder builder)
        {
            builder.MapGet("/all", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            builder.MapGet("/list", GetList).WithDescription("Listado por Región").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            builder.MapGet("/", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CityDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CityDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Ciudad/Municipio"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICityBL cityBL)
        {
            await cityBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CityDTO dto, ICityBL cityBL)
        {
            try
            {
                await cityBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CityDTO dto, ICityBL cityBL)
        {
            try
            {
                await cityBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<CityDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string regionid, string id, ICityBL cityBL)
        {
            return TypedResults.Ok(await cityBL.GetByIdAsync(id.GetGuid(), regionid.GetGuid()));
        }

        private static async Task<Results<Ok<List<CityDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string regionid, ICityBL cityBL)
        {
            return TypedResults.Ok(await cityBL.GetListAsync(regionid.GetGuid()));
        }

        private static async Task<Results<Ok<List<CityDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string regionid, ICityBL cityBL)
        {
            return TypedResults.Ok(await cityBL.GetListAsync(pagination, regionid.GetGuid()));
        }
    }
}
