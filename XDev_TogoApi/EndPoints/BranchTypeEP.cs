using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class BranchTypeEP
    {
        public static RouteGroupBuilder MapBranchType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Tipo Sucursal"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Tipo Sucursal"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Tipo Sucursal"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<BranchTypeDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Tipo Sucursal")); 
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<BranchTypeDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Tipo Sucursal"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Tipo Sucursal"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IBranchTypeBL branchTypeBL)
        {
            await branchTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(BranchTypeDTO dto, IBranchTypeBL branchTypeBL)
        {
            try
            {
                await branchTypeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(BranchTypeDTO dto, IBranchTypeBL branchTypeBL)
        {
            try
            {
                await branchTypeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<BranchTypeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<BranchTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<BranchTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetListAsync(pagination));
        }
    }
}
