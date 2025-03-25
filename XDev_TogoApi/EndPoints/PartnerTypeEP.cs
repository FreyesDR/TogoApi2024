using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerTypeEP
    {
        public static RouteGroupBuilder MapPartnerType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Tipo Socio"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Tipo Socio"));
            return builder;
        }

        private static async Task<Results<Ok<List<PartnerTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPartnerTypeBL partnerTypeBL)
        {
            return TypedResults.Ok(await partnerTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<PartnerTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IPartnerTypeBL partnerTypeBL)
        {
            return TypedResults.Ok(await partnerTypeBL.GetListAsync(pagination));
        }
    }
}
