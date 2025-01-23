using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class CompanyEcoActivityEP
    {
        public static RouteGroupBuilder MapCompanyEconomicActivity(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetEconomicActivities);
            builder.MapGet("/{companyid}/{id}", GetEconomicActivity);
            builder.MapPost("/", CreateEconomicActivity).AddEndpointFilter<ValidationFilter<CompanyEconomicActivityDTO>>(); ;
            builder.MapPut("/", UpdateEconomicActivity).AddEndpointFilter<ValidationFilter<CompanyEconomicActivityDTO>>();
            builder.MapDelete("/{id}", DeleteEconomicActivity);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> DeleteEconomicActivity(string id, ICompanyEconomicActivityBL economicActivityBL)
        {
            await economicActivityBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> UpdateEconomicActivity(CompanyEconomicActivityDTO dto, ICompanyEconomicActivityBL economicActivityBL)
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> CreateEconomicActivity(CompanyEconomicActivityDTO dto, ICompanyEconomicActivityBL economicActivityBL)
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

        private static async Task<Results<Ok<CompanyEconomicActivityDTO>, BadRequest<ExceptionReturnDTO>>> GetEconomicActivity(string companyid, string id, ICompanyEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetByIdAsync(companyid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CompanyEconomicActivityDTO>>, BadRequest<ExceptionReturnDTO>>> GetEconomicActivities(PaginationDTO pagination, string companyid, ICompanyEconomicActivityBL economicActivityBL)
        {
            return TypedResults.Ok(await economicActivityBL.GetListAsync(pagination, companyid));
        }
    }
}
