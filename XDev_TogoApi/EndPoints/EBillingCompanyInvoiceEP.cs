
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingCompanyInvoiceEP
    {
        public static RouteGroupBuilder MapEBillingCompanyInvoice(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination);
            builder.MapGet("/{ebillingid}/{companyid}/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<EBillingCompanyInvoiceDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<EBillingCompanyInvoiceDTO>>();
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(EBillingCompanyInvoiceDTO dto, IEBillingCompanyInvoiceBL eBillingCompany)
        {
            try
            {
                await eBillingCompany.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(EBillingCompanyInvoiceDTO dto, IEBillingCompanyInvoiceBL eBillingCompany)
        {
            try
            {
                await eBillingCompany.CreateAsync(dto);
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

        private static async Task<Results<Ok<EBillingCompanyInvoiceDTO>, BadRequest<ExceptionReturnDTO>>>
            GetById(string ebillingid, string companyid, string id, IEBillingCompanyInvoiceBL companyInvoiceBL)
        {
            return TypedResults.Ok(await companyInvoiceBL.GetByIdAsync(ebillingid.GetGuid(), companyid.GetGuid(), id.GetGuid()));
        }

        private static async Task<Results<Ok<List<EBillingCompanyInvoiceListDTO>>, BadRequest<ExceptionReturnDTO>>>
            GetPagination(PaginationDTO dto, string companyid, IEBillingCompanyInvoiceBL companyInvoiceBL)
        {
            return TypedResults.Ok(await companyInvoiceBL.GetListAsync(dto, companyid));
        }
    }
}
