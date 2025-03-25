using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.DTO.Material;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialFeatureEP
    {
        public static RouteGroupBuilder MapMaterialFeature(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", Get).WithDescription("Obtener").WithMetadata(new ModuleAttribute("Personalización Material"));
            builder.MapPut("/", Update).WithDescription("Modificar").WithMetadata(new ModuleAttribute("Personalización Material"));
            return builder;
        }

        private static async Task<Results<Ok<MaterialFeatureDTO>, BadRequest<ExceptionReturnDTO>>> Update(MaterialFeatureDTO dto, IMaterialFeatureBL featuresBL)
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

        private static async Task<Results<Ok<MaterialFeatureDTO>, BadRequest<ExceptionReturnDTO>>> Get(IMaterialFeatureBL featuresBL)
        {
            return TypedResults.Ok(await featuresBL.GetAsync());
        }
    }
}
