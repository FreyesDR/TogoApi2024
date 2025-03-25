using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.DM;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PaymentConditionEP
    {
        public static RouteGroupBuilder MapPaymentCondition(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Condición de Pago"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Condición de Pago"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Condición de Pago"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PaymentConditionDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Condición de Pago"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PaymentConditionDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Condición de Pago"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Condición de Pago"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPaymentConditionBL paymentConditionBL)
        {
            await paymentConditionBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PaymentConditionDTO dto, IPaymentConditionBL paymentConditionBL)
        {
            try
            {
                await paymentConditionBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PaymentConditionDTO dto, IPaymentConditionBL paymentConditionBL)
        {
            try
            {
                await paymentConditionBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PaymentConditionDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IPaymentConditionBL paymentConditionBL)
        {
            return TypedResults.Ok(await paymentConditionBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PaymentConditionDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPaymentConditionBL paymentConditionBL)
        {
            return TypedResults.Ok(await paymentConditionBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<PaymentConditionListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IPaymentConditionBL paymentConditionBL)
        {
            return TypedResults.Ok(await paymentConditionBL.GetPaymentCondListAsync(pagination));
        }
    }
}
