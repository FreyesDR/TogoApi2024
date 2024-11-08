using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface ICompanyIDBL : IGenericBL<CompanyIDDTO>
    {
        Task<List<CompanyIDDTO>> GetListAsync(PaginationDTO pagination, string companyid);
    }
}
