using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerFeaturesEP
    {
        public static RouteGroupBuilder MapPartnerFeatures(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", Get);
            builder.MapPut("/", Update);
            return builder;
        }

        private static async Task<Results<Ok<PartnerFeaturesDTO>, BadRequest<ExceptionReturnDTO>>> Update(PartnerFeaturesDTO dto, IPartnerFeaturesBL featuresBL)
        {
            try
            {
                await featuresBL.UpdateAsync(dto);
                return TypedResults.Ok(await featuresBL.GetAsync());
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

        private static async Task<Results<Ok<PartnerFeaturesDTO>, BadRequest<ExceptionReturnDTO>>> Get(IPartnerFeaturesBL featuresBL)
        {
            return TypedResults.Ok(await featuresBL.GetAsync());
        }
    }
}
