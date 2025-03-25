
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Admin;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class EndPointPolicyEP
    {
        public static RouteGroupBuilder MapEndPointPolicy(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listado").WithMetadata(new ModuleAttribute("Autorización"));
            return builder;
        }

        private static async Task<Results<Ok<List<EndPointPolicyDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(IEndPointPolicyBL endPointPolicyBL)
        {
            return TypedResults.Ok(await endPointPolicyBL.GetListAsync());
        }
    }
}
