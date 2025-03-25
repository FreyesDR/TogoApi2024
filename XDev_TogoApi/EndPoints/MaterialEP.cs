
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialEP
    {
        public static RouteGroupBuilder MapMaterial(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Material"));
            builder.MapGet("/search/{branchid}", GetActives).WithDescription("Listado por Sucursal").WithMetadata(new ModuleAttribute("Material"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Material"));
            builder.MapGet("/code/{code}", GetByCode).WithDescription("Obtener por Código").WithMetadata(new ModuleAttribute("Material"));            
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MaterialDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Material"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<MaterialDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Material"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Material"));
            return builder;
        }

        private static async Task<Results<Ok<List<MaterialListDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetMaterialListAsync(dto));
        }

        private static async Task<Results<Ok<List<MaterialSaleDTO>>, BadRequest<ExceptionReturnDTO>>> GetActives(string branchid, PaginationDTO dto, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetMaterialActiveListAsync(dto,branchid));
        }

        private static async Task<Results<Ok<MaterialSaleDTO>, NotFound<string>>> GetByCode(string code, string branchid, IMaterialBL materialBL)
        {
            var result = await materialBL.GetByCode(code, branchid);

            if (result is null)
                return TypedResults.NotFound($"Código de material [{code}] no existe o no está ampliado para esta sucursal");

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IMaterialBL materialBL)
        {
            await materialBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(MaterialDTO dto, IMaterialBL materialBL)
        {
            try
            {
                await materialBL.UpdateAsync(dto);
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


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MaterialDTO dto, IMaterialBL materialBL)
        {
            try
            {
                await materialBL.CreateAsync(dto);
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


        private static async Task<Results<Ok<MaterialDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetByIdAsync(id.GetGuid()));
        }

        
    }
}
