using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class CountryEP
    {
        public static RouteGroupBuilder MapCountry(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<CountryDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<CountryDTO>>();
            builder.MapDelete("/{id}", Delete);

            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, ICountryBL countryBL)
        {
            await countryBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(CountryDTO dto, ICountryBL countryBL)
        {
            try
            {
                await countryBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(CountryDTO dto, ICountryBL countryBL)
        {
            try
            {
                await countryBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<CountryDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, ICountryBL countryBL)
        {
            return TypedResults.Ok(await countryBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<CountryDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(ICountryBL countryBL)
        {
            return TypedResults.Ok(await countryBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<CountryDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, ICountryBL countryBL)
        {
            return TypedResults.Ok(await countryBL.GetListAsync(pagination));
        }
    }
}
