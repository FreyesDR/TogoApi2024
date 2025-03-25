using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerRoleEP
    {
        public static RouteGroupBuilder MapPartnerRole(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Roles Socio"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Roles Socio"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtner por Id").WithMetadata(new ModuleAttribute("Roles Socio"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PartnerRoleDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Roles Socio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PartnerRoleDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Roles Socio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Roles Socio"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPartnerRoleBL partnerRoleBL)
        {
            await partnerRoleBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PartnerRoleDTO dto, IPartnerRoleBL partnerRoleBL)
        {
            try
            {
                await partnerRoleBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PartnerRoleDTO dto, IPartnerRoleBL partnerRoleBL)
        {
            try
            {
                await partnerRoleBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PartnerRoleDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IPartnerRoleBL partnerRoleBL)
        {
            return TypedResults.Ok(await partnerRoleBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PartnerRoleDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPartnerRoleBL partnerRoleBL)
        {
            return TypedResults.Ok(await partnerRoleBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<PartnerRoleDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IPartnerRoleBL partnerRoleBL)
        {
            return TypedResults.Ok(await partnerRoleBL.GetListAsync(pagination));
        }
    }
}
