using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class PartnerEP
    {
        public static RouteGroupBuilder MapPartner(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapGet("/code/{companyid}/{code}", GetByCode).WithDescription("Obtener por Sociedad y Código").WithMetadata(new ModuleAttribute("Socio"));            
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<PartnerDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<PartnerDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Socio"));

            // Direcciones
            builder.MapGet("/{partnerid}/address", GetAddresses).WithDescription("Obtener Direcciones").WithMetadata(new ModuleAttribute("Socio"));
            builder.MapGet("/{partnerid}/address/{id}", GetAddress).WithDescription("Obtener Dirección por Id").WithMetadata(new ModuleAttribute("Socio"));            

            return builder;
        }        

        private static async Task<Results<Ok<PartnerDTO>, BadRequest<ExceptionReturnDTO>>> GetByCode(string companyid,string code, IPartnerBL partnerBL)
        {
            try
            {
                return TypedResults.Ok(await partnerBL.GetByCodeAsync(code,companyid));
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


        #region Direcciones
        private static async Task<Results<Ok<AddressDTO>, BadRequest<ExceptionReturnDTO>>> GetAddress(string id, string partnerid, IPartnerBL partnerBL)
        {
            return TypedResults.Ok(await partnerBL.GetAddressById(partnerid, id));
        }

        private static async Task<Results<Ok<List<AddressDTO>>, BadRequest<ExceptionReturnDTO>>> GetAddresses(PaginationDTO pagination, string partnerid, IPartnerBL partnerBL)
        {
            return TypedResults.Ok(await partnerBL.GetAddressAsync(pagination, partnerid));
        }
        #endregion

        #region Socios

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IPartnerBL partnerBL)
        {
            await partnerBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(PartnerDTO dto, IPartnerBL partnerBL)
        {
            try
            {
                await partnerBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(PartnerDTO dto, IPartnerBL partnerBL)
        {
            try
            {
                await partnerBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<PartnerDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IPartnerBL partnerBL)
        {
            return TypedResults.Ok(await partnerBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<PartnerDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IPartnerBL partnerBL)
        {
            return TypedResults.Ok(await partnerBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<PartnerListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IPartnerBL partnerBL)
        {
            return TypedResults.Ok(await partnerBL.GetPartnerListAsync(pagination));
        }
        #endregion
    }
}
