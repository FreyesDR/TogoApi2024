using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerIDEP
    {
        public static RouteGroupBuilder MapPartnerID(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Documento Identificación Socio"));
            builder.MapGet("/{partnerid}/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Documento Identificación Socio")); ;
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PartnerIDDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Documento Identificación Socio")); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PartnerIDDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Documento Identificación Socio")); ;
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Documento Identificación Socio")); ;
            return builder;
        }

        private static async Task<Results<Ok<PartnerIDDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string partnerid, string id, IPartnerIDBL partnerIDBL)
        {
            return TypedResults.Ok(await partnerIDBL.GetByIdAsync(partnerid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PartnerIDDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string partnerid, IPartnerIDBL partnerIDBL)
        {
            return TypedResults.Ok(await partnerIDBL.GetListAsync(pagination, partnerid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPartnerIDBL partnerIDBL)
        {
            await partnerIDBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PartnerIDDTO dto, IPartnerIDBL partnerIDBL)
        {
            try
            {
                await partnerIDBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PartnerIDDTO dto, IPartnerIDBL partnerIDBL)
        {
            try
            {
                await partnerIDBL.CreateAsync(dto);
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
    }
}
