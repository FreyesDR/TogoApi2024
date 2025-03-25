using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerEconomicActivityEP
    {
        public static RouteGroupBuilder MapPartnerEconomicActivity(this RouteGroupBuilder builder) {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Actividad Económica Socio")); 
            builder.MapGet("/{partnerid}/{id}", GetById).WithDescription("Obtner por Id").WithMetadata(new ModuleAttribute("Actividad Económica Socio"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PartnerEconomicActivityDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Actividad Económica Socio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PartnerEconomicActivityDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Actividad Económica Socio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Actividad Económica Socio"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPartnerEconomicActivityBL economicActivityBL)
        {
            await economicActivityBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PartnerEconomicActivityDTO dto, IPartnerEconomicActivityBL economicActivityBL)
        {
            try
            {
                await economicActivityBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PartnerEconomicActivityDTO dto, IPartnerEconomicActivityBL economicActivityBL)
        {
            try
            {
                await economicActivityBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PartnerEconomicActivityDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string partnerid, string id, IPartnerEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetByIdAsync(partnerid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PartnerEconomicActivityDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string partnerid, IPartnerEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetListAsync(pagination, partnerid));
        }
    }
}
