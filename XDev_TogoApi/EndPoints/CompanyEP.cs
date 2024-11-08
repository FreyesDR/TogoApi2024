using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using XDev_TogoApi.Code;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Custom;
using XDev_UnitWork.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace XDev_TogoApi.EndPoints
{
    public static class CompanyEP
    {
        public static RouteGroupBuilder MapCompany(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/{id}", GetById);
            builder.MapGet("/code/{code}", GetByCode);
            builder.MapGet("/list", GetList);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CompanyDTO>>(); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CompanyDTO>>();
            builder.MapDelete("/{id}", Delete);

            // Direcciones
            builder.MapGet("/{companyid}/address", GetAddresses);
            builder.MapGet("/{companyid}/address/{id}", GetAddress);                        

            return builder;
        }

        private static async Task<Results<Ok<CompanyDTO>, NotFound<string>>> GetByCode(string code, ICompanyBL companyBL)
        {
            var result = await companyBL.GetByCode(code);

            if (result is null)
               return TypedResults.NotFound($"Código de sociedad [{code}] no existe");

            return TypedResults.Ok(result);
        }

        #region Direcciones
        private static async Task<Results<Ok<AddressDTO>, BadRequest<ExceptionReturnDTO>>> GetAddress(string id, string companyid, ICompanyBL companyBL)
        {
            return TypedResults.Ok(await companyBL.GetAddressById(companyid, id));
        }

        private static async Task<Results<Ok<List<AddressDTO>>, BadRequest<ExceptionReturnDTO>>> GetAddresses(PaginationDTO pagination, string companyid, ICompanyBL companyBL)
        {
            return TypedResults.Ok(await companyBL.GetAddressAsync(pagination, companyid));
        }
        #endregion

        #region Sociedad
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICompanyBL companyBL)
        {
            await companyBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CompanyDTO dto, ICompanyBL companyBL)
        {
            try
            {
                await companyBL.UpdateAsync(dto);
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


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CompanyDTO dto, ICompanyBL companyBL)
        {
            try
            {
                await companyBL.CreateAsync(dto);
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


        private static async Task<Results<Ok<CompanyDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, ICompanyBL companyBL)
        {
            return TypedResults.Ok(await companyBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CompanyListDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(ICompanyBL companyBL)
        {
            return TypedResults.Ok(await companyBL.GetCompanyListAsync());
        }

        private static async Task<Results<Ok<List<CompanyListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, ICompanyBL companyBL)
        {
            return TypedResults.Ok(await companyBL.GetCompanyListAsync(pagination));
        }
        #endregion
    }
}
