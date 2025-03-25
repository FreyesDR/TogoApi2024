using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;

namespace XDev_TogoApi.EndPoints
{
    public static class EBillingTaxEP
    {
        public static RouteGroupBuilder MapEBillingTax(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Facturación Electrónica Impuestos"));
            return builder;
        }

        private static async Task<Results<Ok<List<EBillingTaxDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, string ebillingid, IEBillingTaxBL eBillingTaxBL)
        {
            return TypedResults.Ok(await eBillingTaxBL.GetListAsync(dto, ebillingid));
        }
    }
}
