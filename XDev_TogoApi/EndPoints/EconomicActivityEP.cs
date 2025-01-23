using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class EconomicActivityEP
    {
        public static RouteGroupBuilder MapEconomicActivity(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<EconomicActivityDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EconomicActivityDTO>>();
            builder.MapDelete("/{id}", Delete);
            builder.MapPost("/upload", UploadFile).DisableAntiforgery();
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>>
            UploadFile(IFormFile file, IEconomicActivityBL activityBL, HttpContext httpContext)
        {
            var stream = httpContext.Request.Form.Files[0].OpenReadStream();
            await activityBL.CreateFromXlsx(stream);

            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IEconomicActivityBL economicActivityBL)
        {
            await economicActivityBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EconomicActivityDTO dto, IEconomicActivityBL economicActivityBL)
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(EconomicActivityDTO dto, IEconomicActivityBL economicActivityBL)
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

        private static async Task<Results<Ok<EconomicActivityDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<EconomicActivityDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<EconomicActivityDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetListAsync(pagination));
        }
    }
}
