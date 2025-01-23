using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;

namespace XDev_TogoApi.EndPoints
{
    public static class CompanyTypeEP
    {
        public static RouteGroupBuilder MapCompanyType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CompanyTypeDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CompanyTypeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICompanyTypeBL companyTypeBL)
        {
            await companyTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CompanyTypeDTO dto, ICompanyTypeBL companyTypeBL)
        {
            try
            {
                await companyTypeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CompanyTypeDTO dto, ICompanyTypeBL companyTypeBL)
        {
            try
            {
                await companyTypeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<CompanyTypeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, ICompanyTypeBL companyTypeBL)
        {
            return TypedResults.Ok(await companyTypeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CompanyTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(ICompanyTypeBL companyTypeBL)
        {
            return TypedResults.Ok(await companyTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<CompanyTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, ICompanyTypeBL companyTypeBL)
        {
            return TypedResults.Ok(await companyTypeBL.GetListAsync(pagination));
        }
    }
}
