
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialBranchEP
    {
        public static RouteGroupBuilder MapMaterialBranch(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Material Sucursal"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MaterialBranchDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Material Sucursal"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<MaterialBranchDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Material Sucursal"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(MaterialBranchDTO dto, IMaterialBranchBL materialBranchBL)
        {
            try
            {
                await materialBranchBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MaterialBranchDTO dto, IMaterialBranchBL materialBranchBL)
        {
            try
            {
                await materialBranchBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<List<MaterialBranchListDTO>>,BadRequest<ExceptionReturnDTO>>> GetAll(string materialid, IMaterialBranchBL materialBranchBL)
        {
            return TypedResults.Ok(await materialBranchBL.GetListAsync(materialid));
        }
    }
}
