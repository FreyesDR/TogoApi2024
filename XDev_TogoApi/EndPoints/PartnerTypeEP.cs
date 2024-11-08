using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerTypeEP
    {
        public static RouteGroupBuilder MapPartnerType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
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
