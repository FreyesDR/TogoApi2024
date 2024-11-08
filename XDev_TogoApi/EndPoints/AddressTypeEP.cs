using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class AddressTypeEP
    {
        public static RouteGroupBuilder MapAddressType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<AddressTypeDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<AddressTypeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }
        
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IAddressTypeBL addressTypeBL)
        {
            await addressTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }
        
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(AddressTypeDTO dto, IAddressTypeBL addressTypeBL)
        {
            try
            {
                await addressTypeBL.UpdateAsync(dto);
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
        
        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(AddressTypeDTO dto, IAddressTypeBL addressTypeBL)
        {
            try
            {
                await addressTypeBL.CreateAsync(dto);
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
        
        private static async Task<Results<Ok<AddressTypeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IAddressTypeBL addressTypeBL)
        {
            return TypedResults.Ok(await addressTypeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<AddressTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IAddressTypeBL addressTypeBL)
        {
            return TypedResults.Ok(await addressTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<AddressTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IAddressTypeBL addressTypeBL)
        {
            return TypedResults.Ok(await addressTypeBL.GetListAsync(pagination));
        }
    }
}
