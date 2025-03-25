using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class WareHouseEP
    {
        public static RouteGroupBuilder MapWareHouse(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Almacén"));            
            builder.MapGet("/list", GetListAll).WithDescription("Listado").WithMetadata(new ModuleAttribute("Almacén"));
            builder.MapGet("/{branchid}/list", GetList).WithDescription("Listado por Sucursal").WithMetadata(new ModuleAttribute("Almacén"));
            builder.MapGet("/{branchid}/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Almacén"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<WareHouseDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Almacén"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<WareHouseDTO>>()
                                       .WithDescription("Actualizar").WithMetadata(new ModuleAttribute("Almacén"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Almacén"));
            return builder;
        }

        private static async Task<Results<Ok<List<WareHouseDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string branchid, IWareHouseBL wareHouseBL)
        {
            return TypedResults.Ok(await wareHouseBL.GetWareHouseListAsync(pagination, branchid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IWareHouseBL wareHouseBL)
        {
            await wareHouseBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(WareHouseDTO dto, IWareHouseBL wareHouseBL)
        {
            try
            {
                await wareHouseBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(WareHouseDTO dto, IWareHouseBL wareHouseBL)
        {
            try
            {
                await wareHouseBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<WareHouseDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string branchid, string id, IWareHouseBL wareHouseBL)
        {
            return TypedResults.Ok(await wareHouseBL.GetByIdAsync(branchid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<WareHouseDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string branchid, IWareHouseBL wareHouseBL)
        {
            return TypedResults.Ok(await wareHouseBL.GetListAsync(branchid));
        }

        private static async Task<Results<Ok<List<WareHouseDTO>>, BadRequest<ExceptionReturnDTO>>> GetListAll(IWareHouseBL wareHouseBL)
        {
            return TypedResults.Ok(await wareHouseBL.GetListAsync());
        }
    }
}
