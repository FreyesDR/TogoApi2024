using XDev_TogoApi.Code;

using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Material;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialTypeEP
    {
        public static RouteGroupBuilder MapMaterialType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Tipo Material"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Tipo Material"));
            builder.MapGet("/{id}", Get).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Tipo Material"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MaterialTypeDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Tipo Material"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<MaterialTypeDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Tipo Material"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Tipo Material"));
            return builder;
        }

        private static async Task<Results<Ok<List<MaterialTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IMaterialTypeBL materialTypeBL)
        {
            return TypedResults.Ok(await materialTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IMaterialTypeBL saleOrderType)
        {
            await saleOrderType.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(MaterialTypeDTO dto, IMaterialTypeBL saleOrderType)
        {
            try
            {
                await saleOrderType.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MaterialTypeDTO dto, IMaterialTypeBL saleOrderType)
        {
            try
            {
                await saleOrderType.CreateAsync(dto);
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

        private static async Task<Results<Ok<MaterialTypeDTO>, BadRequest>> Get(string id, IMaterialTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetByIdAsync(id == "0" ? Guid.Empty : id.GetGuid()));
        }

        private static async Task<Results<Ok<List<MaterialTypeDTO>>, BadRequest>> GetAll(PaginationDTO dto, IMaterialTypeBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync(dto));
        }
    }
}
