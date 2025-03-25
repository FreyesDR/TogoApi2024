using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.DM;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class MeanOfPaymentEP
    {
        public static RouteGroupBuilder MapMeanOfPayment(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Medio de Pago"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Medio de Pago"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Medio de Pago"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MeanOfPaymentDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Medio de Pago"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<MeanOfPaymentDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Medio de Pago"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Medio de Pago"));            
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IMeanOfPaymentBL meanOfPaymentBL)
        {
            await meanOfPaymentBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(MeanOfPaymentDTO dto, IMeanOfPaymentBL meanOfPaymentBL)
        {
            try
            {
                await meanOfPaymentBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MeanOfPaymentDTO dto, IMeanOfPaymentBL meanOfPaymentBL)
        {
            try
            {
                await meanOfPaymentBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<MeanOfPaymentDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IMeanOfPaymentBL meanOfPaymentBL)
        {
            return TypedResults.Ok(await meanOfPaymentBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<MeanOfPaymentDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IMeanOfPaymentBL meanOfPaymentBL)
        {
            return TypedResults.Ok(await meanOfPaymentBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<MeanOfPaymentDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IMeanOfPaymentBL meanOfPaymentBL)
        {
            return TypedResults.Ok(await meanOfPaymentBL.GetListAsync(pagination));
        }
    }
}
