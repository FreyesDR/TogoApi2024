
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialWareHouseEP
    {
        public static RouteGroupBuilder MapMaterialWareHouse(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Material Almacén"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MaterialWareHouseDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Material Almacén"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MaterialWareHouseDTO dto, IMaterialWareHouseBL materialWareHouseBL)
        {
            try
            {
                await materialWareHouseBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<List<MaterialWareHouseListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(string materialid, string branchid, IMaterialWareHouseBL materialWareHouseBL)
        {
            return TypedResults.Ok(await materialWareHouseBL.GetListAsync(materialid, branchid));
        }
    }
}
