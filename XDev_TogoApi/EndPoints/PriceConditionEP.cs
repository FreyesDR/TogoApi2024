using XDev_TogoApi.Code;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.PriceScheme;

namespace XDev_TogoApi.EndPoints
{
    public static class PriceConditionEP
    {
        public static RouteGroupBuilder MapPriceCondition(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Condición de Precio"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Condición de Precio"));
            builder.MapGet("/{id}", Get).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Condición de Precio"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PriceConditionDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Condición de Precio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PriceConditionDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Condición de Precio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Condición de Precio"));
            return builder;
        }

        private static async Task<Results<Ok<List<PriceConditionDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPriceConditionBL priceConditionBL)
        {
            return TypedResults.Ok(await priceConditionBL.GetListAsync());
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPriceConditionBL saleOrderType)
        {
            await saleOrderType.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PriceConditionDTO dto, IPriceConditionBL saleOrderType)
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PriceConditionDTO dto, IPriceConditionBL saleOrderType)
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

        private static async Task<Results<Ok<PriceConditionDTO>, BadRequest>> Get(string id, IPriceConditionBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetByIdAsync(id == "0" ? Guid.Empty : id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PriceConditionDTO>>, BadRequest>> GetAll(PaginationDTO dto, IPriceConditionBL invoiceType)
        {
            return TypedResults.Ok(await invoiceType.GetListAsync(dto));
        }
    }
}
