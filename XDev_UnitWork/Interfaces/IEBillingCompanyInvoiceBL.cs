using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Interfaces
{
    public interface IEBillingCompanyInvoiceBL : IGenericBL<EBillingCompanyInvoiceDTO>
    {
        Task<List<EBillingCompanyInvoiceListDTO>> GetListAsync(PaginationDTO pagination, string companyid);
    }
}
