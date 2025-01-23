using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class CompanyIDEP
    {
        public static RouteGroupBuilder MapCompanyID(this RouteGroupBuilder builder)
        {
            // Documentos de identificación
            builder.MapGet("/", GetCompanyIDS);
            builder.MapGet("/{companyid}/{id}", GetCompanyID);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CompanyIDDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CompanyIDDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<CompanyIDDTO>, BadRequest<ExceptionReturnDTO>>> GetCompanyID(string companyid, string id, [FromServices] ICompanyIDBL companyIDBL)
        {
            return TypedResults.Ok(await companyIDBL.GetByIdAsync(companyid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CompanyIDDTO>>, BadRequest<ExceptionReturnDTO>>> GetCompanyIDS(PaginationDTO pagination, string companyid, [FromServices] ICompanyIDBL companyIDBL)
        {
            return TypedResults.Ok(await companyIDBL.GetListAsync(pagination, companyid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICompanyIDBL companyIDBL)
        {
            await companyIDBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CompanyIDDTO dto, ICompanyIDBL companyIDBL)
        {
            try
            {
                await companyIDBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CompanyIDDTO dto, ICompanyIDBL companyIDBL)
        {
            try
            {
                await companyIDBL.CreateAsync(dto);
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
