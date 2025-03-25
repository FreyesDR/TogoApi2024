
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_Model.Entities;
using XDev_TogoApi.Code;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.PriceScheme;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PriceSchemeEP
    {
        public static RouteGroupBuilder MapPriceScheme(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PriceSchemeDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PriceSchemeDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Esquema de Precio"));
            return builder;
        }

        private static async Task<Results<Ok<List<PriceSchemeListDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPriceSchemeBL priceSchemeBL)
        {
            return TypedResults.Ok(await priceSchemeBL.GetPriceSchemeListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPriceSchemeBL priceSchemeBL)
        {
            await priceSchemeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PriceSchemeDTO dto, IPriceSchemeBL priceSchemeBL)
        {
            try
            {
                await priceSchemeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PriceSchemeDTO dto, IPriceSchemeBL priceSchemeBL)
        {
            try
            {
                await priceSchemeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PriceSchemeDTO>, BadRequest>> GetById(string id, IPriceSchemeBL priceSchemeBL)
        {
            return TypedResults.Ok(await priceSchemeBL.GetByIdAsync(id == "0" ? Guid.Empty : id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PriceSchemeListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO dto, IPriceSchemeBL priceSchemeBL)
        {
            return TypedResults.Ok(await priceSchemeBL.GetPriceSchemeListAsync(dto));
        }
    }
}
