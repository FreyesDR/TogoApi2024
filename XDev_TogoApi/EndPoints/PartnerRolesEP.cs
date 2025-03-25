using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerRolesEP
    {
        public static RouteGroupBuilder MapPartnerRoles(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Roles del Socio"));
            builder.MapPost("/", Create).WithDescription("Crear").WithMetadata(new ModuleAttribute("Roles del Socio"));
            builder.MapDelete("/{partnerid}/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Roles del Socio"));
            return builder;
        }

        private static async Task<Results<Ok<List<PartnerRolesDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(string partnerid, IPartnerRolesBL partnerRolesBL)
        {
            return TypedResults.Ok(await partnerRolesBL.GetListAsync(partnerid.GetGuid()));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string partnerid, string id, IPartnerRolesBL partnerRolesBL)
        {
            await partnerRolesBL.DeleteAsync(partnerid.GetGuid(), id.GetGuid());
            return TypedResults.Ok();
        }
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(string partnerid, string id, IPartnerRolesBL partnerRolesBL)
        {
            try
            {
                await partnerRolesBL.CreateAsync(new PartnerRolesDTO { PartnerId = partnerid.GetGuid(), RoleId = id.GetGuid() });
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
