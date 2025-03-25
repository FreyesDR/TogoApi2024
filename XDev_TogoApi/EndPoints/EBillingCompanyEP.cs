
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Business;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_TogoApi.Code;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingCompanyEP
    {
        public static RouteGroupBuilder MapEBillingCompany(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Facturación Electrónica Sociedad"));
            builder.MapGet("/{ebillingid}/{companyid}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Facturación Electrónica Sociedad"));            
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<EBillingCompanyDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Facturación Electrónica Sociedad"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EBillingCompanyDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Facturación Electrónica Sociedad"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EBillingCompanyDTO dto, IEBillingCompanyBL eBillingCompanyBL)
        {
            try
            {
                await eBillingCompanyBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(EBillingCompanyDTO dto,IEBillingCompanyBL eBillingCompanyBL)
        {
            try
            {
                await eBillingCompanyBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<EBillingCompanyDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string companyid, string ebillingid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetByIdAsync(ebillingid.GetGuid(), companyid.GetGuid()));
        }

        private static async Task<Results<Ok<List<EBillingCompanyListDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, string ebillingid, IEBillingCompanyBL eBillingCompanyBL)
        {
            return TypedResults.Ok(await eBillingCompanyBL.GetListAsync(dto,ebillingid));
        }
    }
}
